// Rozbijamy interfejs na "klocki" (Capabilities)
public interface IDrukarka
{
    void Drukuj(string tresc);
}

public interface ISkaner
{
    void Skanuj(string dokument);
}

public interface IFax
{
    void Faksuj(string tresc, string numer);
}

// Xerox implementuje wszystko (można użyć interfejsu zbiorczego lub wielu osobno)
public class SuperXerox : IDrukarka, ISkaner, IFax
{
    public void Drukuj(string tresc) => Console.WriteLine($"Xerox drukuje: {tresc}");
    public void Skanuj(string dokument) => Console.WriteLine($"Xerox skanuje: {dokument}");
    public void Faksuj(string tresc, string numer) => Console.WriteLine($"Xerox faksuje do {numer}");
}

// Stara drukarka implementuje TYLKO to, co potrafi
// Brak metod Skanuj() i Faksuj() = brak pustego kodu i wyjątków!
public class StaraDrukarkaHP : IDrukarka
{
    public void Drukuj(string tresc) => Console.WriteLine($"HP drukuje: {tresc}");
}

public class Program
{
    public static void Main()
    {
        var drukarki = new List<IDrukarka> { new SuperXerox(), new StaraDrukarkaHP() };
        var skanery = new List<ISkaner> { new SuperXerox() }; // HP tu nie pasuje, i dobrze!

        Console.WriteLine("--- Drukowanie ---");
        foreach (var d in drukarki) d.Drukuj("Raport");

        Console.WriteLine("\n--- Skanowanie ---");
        foreach (var s in skanery) s.Skanuj("Faktura");
    }
}