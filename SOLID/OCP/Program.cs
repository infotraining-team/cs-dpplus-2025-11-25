namespace OCP
{
    //public enum ShapeType
    //{
    //    Circle,
    //    Square
    //}

    //public class Shape
    //{
    //    public ShapeType Type { get; set; }
    //    public double Size { get; set; }

    //    public double Area()
    //    {
    //        switch (Type)
    //        {
    //            case ShapeType.Circle:
    //                return Math.PI * Size * Size;
    //            case ShapeType.Square:
    //                return Size * Size;
    //            default:
    //                throw new NotSupportedException("Nieobsługiwany typ kształtu");
    //        }
    //    }
    //}

    public abstract class Shape
    {
        public abstract double Area();
    }
    public class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            Radius = radius;
        }
        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }
    }

    public class Square: Shape
    {
        public double SideLength { get; set; }
        public Square(double sideLength)
        {
            SideLength = sideLength;
        }
        public override double Area()
        {
            return SideLength * SideLength;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Circle(4));
            shapes.Add(new Square(4));

            foreach (var shape in shapes)
            {
                Console.WriteLine($"Pole {shape.Area()}");
            }
        }
    }
}
