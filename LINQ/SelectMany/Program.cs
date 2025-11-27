namespace SelectMany
{
    public class Order
    {
        public int OrderId { get; set; }
        // Zauważ: Właściwość sama w sobie jest listą!
        public List<string> Items { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1, Items = new List<string> { "Laptop", "Mouse" } },
                new Order { OrderId = 2, Items = new List<string> { "Headphones" } },
                new Order { OrderId = 3, Items = new List<string> { "Cable", "Monitor", "Keyboard" } }
            };

            var res = orders.Select(o => o.Items);
            foreach (var items in res)
            {
                //Console.WriteLine(items);
            }

            var allItem = orders.SelectMany(o=> o.Items).ToList();
            foreach (var item in allItem)
            {
                Console.WriteLine(item);
            }
        }
    }
}
