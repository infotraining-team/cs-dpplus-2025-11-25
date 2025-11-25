namespace LSP
{
    abstract class Shape
    {
        public abstract double Area();
    }

    class Rectangle : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public override double Area()
        {
            return Width * Height;
        }
    }

    class Square : Rectangle
    {
        public double Width
        {
            get => base.Width;
            set => base.Width = base.Height = value;
        }

        public double Height
        {
            get => base.Width;
            set => base.Height = base.Width = value;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var rect = new Square();
            rect.Width = 10;
            rect.Height = 5;
            Console.WriteLine(rect.Area());
        }
    }
}
