using System;

public class DiscountCalculator
{
    public double CalculateDiscountedPrice(double price, string clientType)
    {
        switch (clientType)
        {
            case "Regular":
                // Brak zniżki
                return price;

            case "Premium":
                // 10% zniżki
                return price * 0.90;

            case "VIP":
                // 20% zniżki
                return price * 0.80;

            default:
                throw new ArgumentException("Nieznany typ klienta");
        }
    }
}

public class Program
{
    public static void Main()
    {
        var calculator = new DiscountCalculator();

        double price = 100.00;

        Console.WriteLine($"Regular: {calculator.CalculateDiscountedPrice(price, "Regular")}");
        Console.WriteLine($"VIP: {calculator.CalculateDiscountedPrice(price, "VIP")}");

        // Zadanie: Dodaj nowy typ rabatu "Holiday" (50% zniżki) bez edycji instrukcji switch powyżej!
    }
}