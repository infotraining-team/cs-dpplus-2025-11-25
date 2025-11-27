namespace LINQ_start
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 5, 12, 3, 20, 8, 15 };

            List<int> result = new List<int>();

            foreach (var n in numbers)
            {
                if (n > 10 && n % 2 == 0)
                {
                    result.Add(n);
                }
            }
            result.Sort();
            result.Reverse();
            foreach (var n in result)
            {
                Console.WriteLine(n);
            }

            // --- LINQ

            var linqResult = numbers
                .Where(n => n > 10 && n % 2 == 0)
                .OrderByDescending(n => n);
                

            foreach (var n in linqResult)
            {
                Console.WriteLine(n);
            }


        }
    }
}
