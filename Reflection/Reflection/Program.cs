namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type t1 = typeof(DateTime);
            Console.WriteLine($"t1 {t1.BaseType}");
            Console.WriteLine($"t1 {t1.Assembly}");
            Console.WriteLine($"t1 {t1.Name}");
            Console.WriteLine($"t1 {t1.FullName}");

            string name = "System.DateTime";
            Type? type = Type.GetType(name);
            Console.WriteLine($"{type.Assembly}");

            if (type != null)
            {
                dynamic datetime = Activator.CreateInstance(type);
                Console.WriteLine(datetime.DayOfWeek);
            }
        }
    }
}
