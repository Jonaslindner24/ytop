using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Numerics;

namespace ytop;

class Program
{
    static void Main()
    {
        string UserName = Environment.UserName;  
        string[] meminfo = File.ReadAllLines("/proc/meminfo");
        string[] memtotal_parts = Array.Empty<string>();
        double memtotal_num = 0;
        double memav_num = 0;
        
        foreach (string line in meminfo)
        {
            if (line.StartsWith("MemTotal"))
            {
                memtotal_parts = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                memtotal_num = Convert.ToDouble(memtotal_parts[1]);
            }
            if (line.StartsWith("MemAvailable"))
            {
                memtotal_parts = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                memav_num = Convert.ToDouble(memtotal_parts[1]);
            }
        }

        double memfree_num = memtotal_num - memav_num;

        memtotal_num = Math.Round(memtotal_num / 1000000); 
        memfree_num = Math.Round(memfree_num / 10000);
        memav_num = Math.Round(memav_num / 10000);


        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("======== ytop v1.0 ========");

        Console.WriteLine($"Hello: {UserName}");
        Console.WriteLine($"Memory Total:.....{memtotal_num} GB");
        Console.WriteLine($"Memory in use:....{memav_num} kb");
        Console.WriteLine($"Memory Free:......{memfree_num} kb");
        Console.WriteLine();
        Console.ResetColor();

        
    }
}