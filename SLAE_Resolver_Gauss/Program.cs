using System;
using System.IO;

namespace SLAE_Resolver_Gauss
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter SLAE.txt full name:");
            var filePath = Console.ReadLine();
            var lines = File.ReadAllLines(filePath);

            var n = lines.Length;
            var m = new double[n, n];
            var v = new double[n];

            for(int i = 0; i < n; i++)
            {
                var cells = lines[i].Split('|');
                for (int j = 0; j < n; j++)
                {
                    m[i, j] = double.Parse(cells[j]);
                }
                v[i] = double.Parse(cells[lines.Length]);
            }

            var t = DateTime.Now;
            var result = Gauss.Solve(m, v);
            var ts = DateTime.Now - t;

            for(int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"x[{i}] = {Math.Round(result[i])}");
            }
            Console.WriteLine($"Solved in {ts}");
        }
    }
}
