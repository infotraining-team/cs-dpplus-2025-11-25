using Microsoft.VisualBasic;

namespace SimpleAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var t= SlowTask();
            var t2 = MyAsyncTask();
            await t;
            var res = await t2;
            Console.WriteLine($"{res}");
            Console.WriteLine("Program is going to end");
        }

        public static async Task SlowTask()
        {
            Console.WriteLine("Slow Task");
            await Task.Delay(1000);
            Console.WriteLine("Slow Task Ended");
        }

        public static async Task<int> MyAsyncTask()
        {
            Console.WriteLine("Async function");
            await AnotherTask();
            return 42;
        }

        public static async Task AnotherTask()
        {
            Console.WriteLine("Another Task");
        }
    }
}
