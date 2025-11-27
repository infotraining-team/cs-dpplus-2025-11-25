using System;
using System.Collections.Generic;
using System.Linq;

public enum HeroClass { Warrior, Mage, Rogue, Cleric }

public record Item(string Name, int Value, bool IsMagic);
public record Hero(string Name, HeroClass Class, int Level, List<Item> Inventory);
public record Player(string UserName, string Country, bool IsPremium, List<Hero> Heroes);

public class Program
{
    public static void Main()
    {
        var players = GetGameData();

        /*
        High-Level Premium: Znajdź nazwy wszystkich bohaterów (tylko nazwy),
        którzy mają poziom wyższy niż 40, ale należą tylko do graczy Premium.*/

        var q1 = players
            .Where(p => p.IsPremium)
            .SelectMany(p => p.Heroes)
            .Where(h => h.Level > 40)
            .Select(h => h.Name);

        Console.WriteLine(string.Join(", ", q1));

        /*Inventory Inspector (SelectMany): Wypisz nazwy wszystkich magicznych przedmiotów
        (IsMagic == true) posiadanych przez wszystkich graczy z Polski.
        Lista ma być płaska (bez podziału na graczy).*/

        var q2 = players
            .Where(p => p.Country == "Poland")
            .SelectMany(p => p.Heroes)
            .SelectMany(h => h.Inventory)
            .Where(i => i.IsMagic)
            .Select(i => i.Name);

        Console.WriteLine(string.Join(", ", q2));

        /*Class Balance (GroupBy): Policz, ilu jest bohaterów w każdej z klas (Warrior, Mage etc.).
        Wynik powinien wyglądać np.: Mage: 2, Warrior: 2.*/

        Console.WriteLine("\n--- ZADANIE 3: Hero Count by Class ---");
        var q3 = players
            .SelectMany(p => p.Heroes)
            .GroupBy(h => h.Class)                 
            .Select(g => new { Class = g.Key, Count = g.Count() });

        foreach (var item in q3)
            Console.WriteLine($"{item.Class}: {item.Count}");

        /*The Richest Player (Sum & Aggregation): Znajdź gracza, który posiada łącznie "najdroższy" inwentarz
        (suma Value wszystkich przedmiotów u wszystkich jego bohaterów). Wypisz nick gracza i łączną wartość.*/

        Console.WriteLine("\n--- ZADANIE 4: Richest Player ---");
        var q4 = players
            .Select(p => new
            {
                Name = p.UserName,
                TotalWealth = p.Heroes
                    .SelectMany(h => h.Inventory)
                    .Sum(i => i.Value)
            })
            .OrderByDescending(x => x.TotalWealth)   // Sortuj malejąco
            .FirstOrDefault();                       // Weź pierwszego

        Console.WriteLine($"Winner: {q4?.Name} with ${q4?.TotalWealth}");

        /*Average Level (Bonus): Oblicz średni poziom bohatera dla graczy z każdego kraju.
         */
        var q5 = players
            .GroupBy(p => p.Country)
            .Select(g => new
            {
                Country = g.Key,
                AvgLevel = g.SelectMany(p => p.Heroes).Average(h => h.Level)
            });

        foreach (var item in q5)
            Console.WriteLine($"{item.Country}: {item.AvgLevel:F1}");
    }

    public static List<Player> GetGameData()
    {
        var p1 = new Player("SlayerPL", "Poland", true, new List<Hero> {
            new Hero("Geralt", HeroClass.Warrior, 50, new List<Item> { new Item("Steel Sword", 100, false), new Item("Silver Sword", 300, true) }),
            new Hero("Yen", HeroClass.Mage, 45, new List<Item> { new Item("Staff", 150, true), new Item("Potion", 10, true) })
        });

        var p2 = new Player("NoobMaster", "USA", false, new List<Hero> {
            new Hero("Rambo", HeroClass.Warrior, 5, new List<Item> { new Item("Stick", 1, false) })
        });

        var p3 = new Player("GandalfTheGrey", "UK", true, new List<Hero> {
            new Hero("Mithrandir", HeroClass.Mage, 90, new List<Item> { new Item("Glamdring", 500, true), new Item("Fireworks", 50, false) })
        });

        var p4 = new Player("Healer99", "Poland", false, new List<Hero> {
            new Hero("Mercy", HeroClass.Cleric, 30, new List<Item> { new Item("Bandage", 5, false) }),
            new Hero("Loki", HeroClass.Rogue, 60, new List<Item> { new Item("Dagger", 200, false), new Item("Cloak", 100, true) })
        });

        return new List<Player> { p1, p2, p3, p4 };
    }
}