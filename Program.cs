using System;
using System.IO;
using NeoGemino.Templates;

namespace NeoGemino
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Data/Data.json"));
            var outputDirectory = Path.Combine(Environment.CurrentDirectory, "dist");
            new Sample(data).Render(outputDirectory);

            Console.WriteLine("Execution completed");
        }
    }
}
