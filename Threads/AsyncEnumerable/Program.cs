using System.Formats.Asn1;

namespace AsyncEnumerable
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            await foreach (var n in StreamAsync())
            {
                Console.WriteLine($"Got {n}");
            }
        }

        static async IAsyncEnumerable<int> StreamAsync()
        {
            for (int i = 0; i < 10; ++i)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }
}
