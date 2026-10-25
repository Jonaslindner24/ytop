using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Diagnostics.Contracts;

namespace ytop;

class Program
{
    static void Main()
    {
        string UserName = Environment.UserName;
        string[] meminfo = File.ReadAllLines("/proc/meminfo");
        string[] cpuinfo = File.ReadAllLines("/proc/cpuinfo");
        string[] cpuinfo_parts = Array.Empty<string>();
        string[] memtotal_parts = Array.Empty<string>();
        string cpuname = "";
        double memtotal_num = 0;
        double memav_num = 0;
        var monitor = new Pfcount();
        double usage = monitor.GetUsagePercent();

        foreach (string line in meminfo)
        {
            if (line.StartsWith("MemTotal"))
            {
                memtotal_parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                memtotal_num = Convert.ToDouble(memtotal_parts[1]);
            }
            if (line.StartsWith("MemAvailable"))
            {
                memtotal_parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                memav_num = Convert.ToDouble(memtotal_parts[1]);
            }
        }

        foreach (string line in cpuinfo)
        {
            if (line.StartsWith("model name"))
            {
                cpuinfo_parts = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                cpuname = cpuinfo_parts[1];
            }
        }



        double memuse_num = memtotal_num - memav_num;

        memtotal_num = Math.Round(memtotal_num / 1000);
        memuse_num = Math.Round(memuse_num / 1000);
        memav_num = Math.Round(memav_num / 1000);


        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("======== ytop v1.0 ========");


        Console.WriteLine($"Hello: {UserName}");
        Console.WriteLine();
        Console.WriteLine("----------- Memory Info -------------");
        Console.WriteLine($"Memory Total:.....{memtotal_num} mb");
        Console.WriteLine($"Memory in use:....{memuse_num} mb");
        Console.WriteLine($"Memory Free:......{memav_num} mb");
        Console.WriteLine();
        Console.WriteLine("----------- Cpu Info -------------");
        Console.WriteLine($"Model name:{cpuname}");
        Console.WriteLine($"Total usage: {usage:F1}%");

        Console.WriteLine();
        Console.ResetColor();


    }
}