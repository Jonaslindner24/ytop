using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;

namespace ytop;

class Program
{
    static void Main()
    {
        string UserName = Environment.UserName;  
        string[] meminfo = File.ReadAllLines("/proc/meminfo");
        string[] meminfo_parts;
        
        foreach (string line in meminfo)
        {
            if (line.StartsWith("MemTotal"))
            {
                meminfo_parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }

        Int32 memtotal = Convert.ToInt32(meminfo_parts[1]);

        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("======== ytop v1.0 ========");
        Console.ResetColor();

        Console.WriteLine($"Hello: {UserName}");
        Console.WriteLine();

        
    }
}