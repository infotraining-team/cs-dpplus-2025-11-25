using System.Net;

namespace LineCounter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string targetDirectory = "C:\\code\\cs-dpplus-2025-11-25";
            var files = Directory.GetFiles(targetDirectory, "*.cs",
                SearchOption.AllDirectories);
            long counter = 0;
            foreach (var file in files)
            {
                counter += CountLines(file);
            }

            List<Task<int>> tasks = new List<Task<int>>();
            foreach (var file in files)
            {
                tasks.Add(CountLinesAsync(file));
            }

            int[] lineCounts = await Task.WhenAll(tasks);



            Console.WriteLine($"Number of lines = {lineCounts.Sum()}");
        }

        static int CountLines(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            return lines.Length;
        }

        static async Task<int> CountLinesAsync(string filename)
        {
            string[] lines = await File.ReadAllLinesAsync(filename);
            return lines.Length;
        }
    }
}
