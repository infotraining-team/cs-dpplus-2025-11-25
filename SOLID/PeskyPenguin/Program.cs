using System;
using System.Collections.Generic;

// Wspólna baza (co łączy orła i pingwina?)
public abstract class Ptak
{
    public string Gatunek { get; set; }
    public void Jedz() => Console.WriteLine("Ptak dziobie jedzenie.");
}

// Interfejs dla "umiejętności", a nie "bycia"
public interface ILatajacy
{
    void Lec();
}

// Orzeł jest Ptakiem I potrafi latać
public class Orzel : Ptak, ILatajacy
{
    public void Lec() => Console.WriteLine("Orzeł szybuje.");
}

// Pingwin jest Ptakiem, ale NIE implementuje ILatajacy
public class Pingwin : Ptak
{
    public void Plywaj() => Console.WriteLine("Pingwin pływa.");
}

public class Program
{
    public static void Main()
    {
        // 1. Lista wszystkich ptaków (do karmienia)
        var zoo = new List<Ptak> { new Orzel(), new Pingwin() };

        Console.WriteLine("--- Pora karmienia ---");
        foreach (var p in zoo) p.Jedz(); // To zadziała dla wszystkich

        // 2. Lista tylko latających (do pokazu lotniczego)
        // Kompilator NIE POZWOLI dodać tu Pingwina. 
        // Błąd wyłapujemy w czasie pisania kodu (Compile Time), a nie działania (Runtime).
        var strefaLotow = new List<ILatajacy>
        {
            new Orzel() 
            // new Pingwin() <--- To się nawet nie skompiluje! I o to chodzi!
        };

        Console.WriteLine("\n--- Pokaz lotniczy ---");
        foreach (var lotnik in strefaLotow)
        {
            lotnik.Lec();
        }
    }
}