using System;

public interface IDiscount
{
    double ApplyDiscount(double orignalPrice);
}

public class DiscountCalculator
{
    public double CalculateDiscountedPrice(double price, IDiscount discount)
    {
        return discount.ApplyDiscount(price);
    }
}

public class BlackFriday : IDiscount
{
    public double ApplyDiscount(double orignalPrice)
    {
        return orignalPrice * 0.5;
    }
}

public class VIP : IDiscount
{
    public double ApplyDiscount(double orignalPrice)
    {
        return orignalPrice * 0.9;
    }
}

public class Christmas : IDiscount
{
    public double ApplyDiscount(double orignalPrice)
    {
        return orignalPrice * 1.1;
    }
}

public class DictionaryDiscountFactory
{
    private Dictionary<string, IDiscount> _strategy = new Dictionary<string, IDiscount>
    {
        { "blackfriday", new BlackFriday() },
        { "vip", new VIP() }
    };
    public IDiscount GetStrategy(string key)
    {
        if (_strategy.TryGetValue(key, out var strategy))
        {
            return strategy;
        }

        throw new Exception("No discount");
    }

    public void Register(string id, IDiscount strategy)
    {
        _strategy.Add(id, strategy);
    }
}

public class Program
{
    public static void Main()
    {
        var calculator = new DiscountCalculator();

        double price = 100.00;
        var factory = new DictionaryDiscountFactory();
        factory.Register("christmas", new Christmas());
        //Console.WriteLine($"Regular: {calculator.CalculateDiscountedPrice(price, )}");
        //Console.WriteLine($"VIP: {calculator.CalculateDiscountedPrice(price, "VIP")}");
        Console.WriteLine($"BF: {calculator.CalculateDiscountedPrice(price, factory.GetStrategy("vip"))}");
        Console.WriteLine($"BF: {calculator.CalculateDiscountedPrice(price, factory.GetStrategy("christmas"))}");

    }

    // Zadanie: Dodaj nowy typ rabatu "BlackFriday" (50% zniżki) bez edycji instrukcji switch powyżej!

}