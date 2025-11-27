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
        }
    }
}
