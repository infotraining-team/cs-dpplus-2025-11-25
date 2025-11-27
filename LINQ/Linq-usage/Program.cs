using System.ComponentModel.DataAnnotations;

namespace Linq_usage
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "Laptop", Category = "Electronics", Price = 2500 },
                new Product { Name = "Headphones", Category = "Electronics", Price = 150 },
                new Product { Name = "Coffee", Category = "Groceries", Price = 30 },
                new Product { Name = "Desk", Category = "Furniture", Price = 400 },
                new Product { Name = "Mouse", Category = "Electronics", Price = 50 },
                new Product { Name = "Tea", Category = "Groceries", Price = 15 },
                new Product { Name = "Monitor", Category = "Electronics", Price = 800 }
            };

            var expensive = products
                .Where(p => p.Category == "Electronics" && p.Price > 200)
                .OrderBy(p => p.Name);

            foreach (Product p in expensive)
            {
                Console.WriteLine(p.Name);
            }

            var cheapest = products
                .Where(p => p.Category == "Groceries")
                .OrderBy(p => p.Price)
                .FirstOrDefault();

            Console.WriteLine($"Cheapest = {cheapest.Name}");

            var productSummary = products
                .Where(p => p.Price > 100)
                .OrderByDescending(p => p.Price)
                .Select(p => new { p.Name, p.Price })
                .ToList();

            foreach (var item in productSummary)
            {
                Console.WriteLine($"{item.Name} costs {item.Price}");
            }

            //---- GrouBy

            var productGroups = products
                .GroupBy(p => p.Category);

            foreach (var group in productGroups)
            {
                Console.WriteLine($"Category : {group.Key}");
                foreach (var prod in group)
                {
                    Console.WriteLine($" ---- {prod.Name}");
                }
            }

            foreach (var group in productGroups)
            {
                Console.WriteLine($"Category : {group.Key}");
                Console.WriteLine($" Average price {group.Average(p=> p.Price)}");
            }
        }
    }
}
