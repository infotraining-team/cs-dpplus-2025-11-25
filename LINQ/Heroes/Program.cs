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
        którzy mają poziom wyższy niż 40, ale należą tylko do graczy Premium.

        Inventory Inspector (SelectMany): Wypisz nazwy wszystkich magicznych przedmiotów 
        (IsMagic == true) posiadanych przez wszystkich graczy z Polski.
        Lista ma być płaska (bez podziału na graczy).

        Class Balance (GroupBy): Policz, ilu jest bohaterów w każdej z klas (Warrior, Mage etc.).
        Wynik powinien wyglądać np.: Mage: 2, Warrior: 2.

        The Richest Player (Sum & Aggregation): Znajdź gracza, który posiada łącznie "najdroższy" inwentarz
        (suma Value wszystkich przedmiotów u wszystkich jego bohaterów). Wypisz nick gracza i łączną wartość.

        Average Level (Bonus): Oblicz średni poziom bohatera dla graczy z każdego kraju.
         */

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