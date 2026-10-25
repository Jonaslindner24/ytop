using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;
using System.Threading;


namespace ytop;

public class Pfcount
{
    private long[] ReadCpuCounters(){

        string line = File.ReadLines("/proc/stat") .First(line => line.StartsWith("cpu", StringComparison.Ordinal));
        
        return line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
         .Skip(1)
         .Select(value => long.Parse(value, CultureInfo.InvariantCulture))
         .ToArray(); 
    }

    public double GetUsagePercent (int intervamilliseconds = 500)
    {
        long[] before = ReadCpuCounters();
        Thread.Sleep(intervamilliseconds);
        long[] after = ReadCpuCounters();

        long totalbefore = before.Sum();
        long totalafter = after.Sum();

        long idlebefore = before[3] + (before.Length > 4 ? before[4] : 0);
        long idleafter = after[3] + (after.Length > 4 ? after[4] : 0);

        long totaldelta = totalafter - totalbefore;
        long idledelta = idleafter - idlebefore;

        if (totaldelta <= 0)
        {
            return 0;
        }

        return 100.0 * (totaldelta - idledelta) / totaldelta;


    }

}