namespace Aggregate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

            int sum = numbers.Aggregate((acc, next) => acc + next);
            Console.WriteLine(sum);

            List<string> names = new List<string>
            {
                "Ala", "Ola", "Leszek", "Euzebiusz"
            };

            string allNames = names.Aggregate((current, name) => current + " " + name);

            Console.WriteLine(allNames);

            string longestName = names.Aggregate((longestSoFar, name) =>
            {
                if (name.Length > longestSoFar.Length)
                {
                    return name;
                }
                return longestSoFar;
            });
            Console.WriteLine(longestName);
        }
    }
}
