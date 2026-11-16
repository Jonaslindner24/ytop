using System;
using System.Diagnostics;


namespace ytop;

class Getgraphics
{
    public string getgraphics()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = "-c \"/sbin/lspci | grep -i vga\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (Process process = Process.Start(startInfo))
        {
            using (System.IO.StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}