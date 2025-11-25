using System.ComponentModel;
using System.Transactions;

namespace ISP
{
    public interface IShape3d
    {
        public double Volume();
    }

    public interface IShape2d
    {
        public double Area();
    }

    public class Sphere(double radius) : IShape3d, IShape2d
    {
        public double Area()
        {
            return 4.0 * Math.PI * radius * radius;
        }

        public double Volume()
        {
            return 4.0 / 3.0 * Math.PI * (radius * radius * radius);
        }
    }

    public class Circle(double radius) : IShape2d
    {
        public double Area()
        {
            return Math.PI * radius * radius;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
