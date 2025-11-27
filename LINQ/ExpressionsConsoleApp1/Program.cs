using System.Linq.Expressions;

namespace _08_Expressions
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 2500 },
                new Product { Name = "Monitor", Price = 1200 },
                new Product { Name = "Mouse", Price = 50 }
            };

            // Cel: Chcemy zbudować dynamicznie: p => p.Price > 100

            // 1. Definiujemy parametr "p" (jak w lambda p => ...)
            ParameterExpression param = Expression.Parameter(typeof(Product), "p");

            // 2. Definiujemy stałą "100"
            ConstantExpression constant = Expression.Constant(100.0);

            // 3. Wyciągamy właściwość "Price" z parametru "p"
            MemberExpression property = Expression.Property(param, "Price");

            // 4. Budujemy warunek logiczny: p.Price > 100
            BinaryExpression body = Expression.GreaterThan(property, constant);

            // 5. Pakujemy to w całą Lambdę: Func<Product, bool>
            Expression<Func<Product, bool>> lambda =
                Expression.Lambda<Func<Product, bool>>(body, param);

            // --- SPRAWDZENIE ---
            Console.WriteLine($"Constructed Expression: {lambda}");
            // Wynik w konsoli: p => (p.Price > 100)

            Func<Product, bool> compiledFunc = lambda.Compile();

            var expensiveProducts = products.Where(compiledFunc);
            foreach (var prod in expensiveProducts)
            {
                Console.WriteLine(prod.Name);
            }
        }
    }
}