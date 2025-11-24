using System;
using System.Collections.Generic;

// Klasa bazowa - definiuje kontrakt: "Każdy ptak potrafi latać"
public class Ptak
{
    public virtual void Lec()
    {
        Console.WriteLine("Ptak wzbija się w powietrze.");
    }
}

public class Orzel : Ptak
{
    public override void Lec()
    {
        Console.WriteLine("Orzeł szybuje wysoko!");
    }
}

public class Kaczka : Ptak
{
    public override void Lec()
    {
        Console.WriteLine("Kaczka macha skrzydłami nad wodą.");
    }
}

// Ten kod wygląda niewinnie, ale to BOMBA ZEGAROWA
public class Pingwin : Ptak
{
    public override void Lec()
    {
        // Naruszenie LSP: Zmieniamy zachowanie bazowe na błąd!
        throw new NotImplementedException("Pingwiny nie latają! Złamię Ci program!");
    }

    public void Plywaj()
    {
        Console.WriteLine("Pingwin pływa w lodowatej wodzie.");
    }
}

public class Program
{
    public static void Main()
    {
        var ptaszarnia = new List<Ptak>
        {
            new Orzel(),
            new Kaczka(),
            new Pingwin() // Pingwin wygląda jak Ptak, więc lista go przyjmuje
        };

        Console.WriteLine("=== Rozpoczynamy pokaz lotniczy ===");

        foreach (var ptak in ptaszarnia)
        {
            // Tutaj kod jest 'ślepy'. Ufa, że każdy Ptak potrafi latać.
            // Przy Pingwinie nastąpi awaria programu (crash).
            try
            {
                ptak.Lec();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BŁĄD KRYTYCZNY: {ex.Message}");
            }
        }
    }
}