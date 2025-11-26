using System.Collections.Concurrent;

namespace BlockingCollectionFun
{
    internal class Program
    {
        static BlockingCollection<int> _queue = new BlockingCollection<int>(2);
        
        static void Main(string[] args)
        {
            _queue.Add(1);
            _queue.Add(2);
            _queue.Take();
            _queue.Add(3);
            _queue.Take();
            _queue.Take();
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"is 7 prime? {isPrime(7)}");
            Console.WriteLine($"is 6 prime? {isPrime(6)}");

        }

        static bool isPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i < number; ++i)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
