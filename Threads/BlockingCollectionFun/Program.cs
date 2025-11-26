using System.Collections.Concurrent;

namespace BlockingCollectionFun
{
    internal class Program
    {
        static BlockingCollection<int> _queue = new BlockingCollection<int>(10);
        static BlockingCollection<string> _sink = new BlockingCollection<string>(10);


        static void Producer()
        {
            Console.WriteLine("Producer is starting");
            for (int i = 1000_000; i < 1000_100; ++i)
            {
                _queue.Add(i);
                Console.WriteLine($"Producer: just added {i} to queue");
                Thread.Sleep(50);
            }
            _queue.CompleteAdding();
        }

        static void Consumer(int id)
        {
            Console.WriteLine($"Consumer {id} is starting");
            foreach(var n in _queue.GetConsumingEnumerable())
            {
                if (isPrime(n))
                {
                    _sink.Add($"Consumer {id}: {n} is Prime");
                }
            }
        }

        static void Printer()
        {
            foreach (string s in _sink.GetConsumingEnumerable())
            {
                Console.WriteLine(s);
            }
        }

        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            Task prod = Task.Run(() => Producer());
            Task p = Task.Run(() => Printer());
            for (int i = 0; i < 4; ++i)
            {
                int id = i;
                Task cons = Task.Run(() => Consumer(id));
                tasks.Add(cons);
            }
            tasks.Add(prod);
            Console.WriteLine("All started");
            Task.WaitAll(tasks.ToArray());
            _sink.CompleteAdding();
            p.Wait();

        }

        static bool isPrime(int number)
        {
            if (number <= 2) return false;
            for (int i = 2; i < number; ++i)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
