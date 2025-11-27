using System;

// 1. Component
// Defines the common interface for wrappers and the wrapped object.
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

// 2. Concrete Component
// The base object we are going to decorate.
public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Simple Coffee";
    public double GetCost() => 5.00;
}

// 3. Base Decorator
// Has a reference to the wrapped component.
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    // By default, delegate to the wrapped object
    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost() => _coffee.GetCost();
}

// 4. Concrete Decorators
// Adds specific behavior (and cost).

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Milk";
    }

    public override double GetCost()
    {
        return base.GetCost() + 1.50;
    }
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Sugar";
    }

    public override double GetCost()
    {
        return base.GetCost() + 0.50;
    }
}

public class WhipDecorator : CoffeeDecorator
{
    public WhipDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Whip";
    }

    public override double GetCost()
    {
        return base.GetCost() + 2.00;
    }
}

// 5. Client Code
public class Program
{
    public static void Main()
    {
        // Order: A simple coffee
        ICoffee myCoffee = new SimpleCoffee();
        Console.WriteLine($"{myCoffee.GetDescription()} : ${myCoffee.GetCost()}");

        // Order: Coffee with Milk and Sugar
        // Notice how we wrap objects like layers of an onion
        ICoffee myLatte = new MilkDecorator(new SugarDecorator(new SimpleCoffee()));

        Console.WriteLine($"{myLatte.GetDescription()} : ${myLatte.GetCost()}");

        // Order: Double Whip, Milk, Sugar Coffee (Order of wrapping usually doesn't matter for logic, but matters for structure)
        ICoffee fancyCoffee = new WhipDecorator(new WhipDecorator(new MilkDecorator(new SimpleCoffee())));

        Console.WriteLine($"{fancyCoffee.GetDescription()} : ${fancyCoffee.GetCost()}");
    }
}