using System;
using System.Collections.Generic;

// 1. The Visitor Interface
// Declares a visit method for each concrete element class.
public interface IVisitor
{
    void VisitDot(Dot dot);
    void VisitRectangle(Rectangle rectangle);
    void VisitCircle(Circle circle);
}

// 2. The Element Interface
// Declares an `Accept` method that takes the visitor as an argument.
public interface IShape
{
    void Accept(IVisitor visitor);
}

// 3. Concrete Elements
// Each class must implement the `Accept` method.
// CRITICAL: They double-dispatch execution back to the visitor.
public class Dot : IShape
{
    public void Draw() => Console.WriteLine("Drawing a Dot.");

    public void Accept(IVisitor visitor)
    {
        // Compiler knows 'this' is of type 'Dot', so it picks VisitDot.
        visitor.VisitDot(this);
    }
}

public class Rectangle : IShape
{
    public void Draw() => Console.WriteLine("Drawing a Rectangle.");

    public void Accept(IVisitor visitor)
    {
        visitor.VisitRectangle(this);
    }
}

public class Circle : IShape
{
    public void Draw() => Console.WriteLine("Drawing a Circle.");

    public void Accept(IVisitor visitor)
    {
        visitor.VisitCircle(this);
    }
}

// 4. Concrete Visitor (The Logic)
// Instead of modifying shapes to add XML support, we put XML logic here.
public class XmlExportVisitor : IVisitor
{
    public void VisitDot(Dot dot)
    {
        Console.WriteLine($"<dot>No coordinates</dot>");
    }

    public void VisitRectangle(Rectangle rectangle)
    {
        Console.WriteLine($"<rectangle>Complex shape</rectangle>");
    }

    public void VisitCircle(Circle circle)
    {
        Console.WriteLine($"<circle>Round shape</circle>");
    }
}

// Another Visitor (e.g., for calculating statistics)
public class PricingVisitor : IVisitor
{
    public void VisitDot(Dot dot) => Console.WriteLine("Dot cost: $1");
    public void VisitRectangle(Rectangle rectangle) => Console.WriteLine("Rectangle cost: $5");
    public void VisitCircle(Circle circle) => Console.WriteLine("Circle cost: $3");
}

// 5. Client Code
public class Program
{
    public static void Main()
    {
        // The object structure (usually a list, tree, or graph)
        var shapes = new List<IShape>
        {
            new Dot(),
            new Circle(),
            new Rectangle(),
            new Dot()
        };

        // 1. We want to export to XML
        var xmlVisitor = new XmlExportVisitor();

        Console.WriteLine("--- Exporting to XML ---");
        foreach (var shape in shapes)
        {
            // Client doesn't need to know the specific type of shape.
            // Double dispatch handles the rest.
            shape.Accept(xmlVisitor);
        }

        // 2. Requirements changed! We need pricing now.
        // We create a new visitor WITHOUT changing the Shape classes.
        var pricingVisitor = new PricingVisitor();

        Console.WriteLine("\n--- Calculating Costs ---");
        foreach (var shape in shapes)
        {
            shape.Accept(pricingVisitor);
        }
    }
}