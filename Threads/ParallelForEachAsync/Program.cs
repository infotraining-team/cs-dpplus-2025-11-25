using System;
using System.Diagnostics;

namespace ParallelForEachAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<string> urls = new List<string>();
            urls.Add("gooogle.com");
            urls.Add("wp.pl");
            urls.Add("onet.pl");

            var sw = Stopwatch.StartNew();
            //foreach (var url in urls)
            //{
            //    await processUrlAsyncTask(url);
            //}
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount // e.g., 4, 8, 16
            };

            var token = new CancellationToken();

            await Parallel.ForEachAsync(urls, async (url, token) =>
            {
                await processUrlAsyncTask(url, token);
            });
            sw.Stop();
            Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}");
        }

        static void processUrl(string url)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"{url} processed");
        }

        static async Task processUrlAsyncTask(string url, CancellationToken token)
        {
            await Task.Delay(1000, token);
            Console.WriteLine($"{url} processed");
        }
    }
}
