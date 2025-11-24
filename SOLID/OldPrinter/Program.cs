using System;
using System.Collections.Generic;

// "Tłusty" interfejs (Fat Interface)
// Wymusza na wszystkim, co jest maszyną, umiejętność faksowania i skanowania.
public interface IMaszynaBiurowa
{
    void Drukuj(string tresc);
    void Skanuj(string dokument);
}

// Super maszyna - implementuje wszystko poprawnie
public class SuperXerox : IMaszynaBiurowa
{
    public void Drukuj(string tresc) => Console.WriteLine($"Xerox drukuje: {tresc}");
    public void Skanuj(string dokument) => Console.WriteLine($"Xerox skanuje: {dokument}");
}

// Tania drukarka - ofiara złego interfejsu
public class StaraDrukarkaHP : IMaszynaBiurowa
{
    public void Drukuj(string tresc)
    {
        Console.WriteLine($"HP drukuje: {tresc}");
    }

    public void Skanuj(string dokument)
    {
        // Muszę to zaimplementować, bo interfejs mnie zmusza...
        throw new NotImplementedException("Ta drukarka nie ma skanera!");
    }
}

public class Program
{
    public static void Main()
    {
        var biuro = new List<IMaszynaBiurowa>
        {
            new SuperXerox(),
            new StaraDrukarkaHP()
        };

        Console.WriteLine("--- Rozpoczynamy pracę biurową ---");

        foreach (var maszyna in biuro)
        {
            // Próbujemy użyć wszystkich funkcji (bo interfejs obiecuje, że są!)
            maszyna.Drukuj("Raport roczny");

            try
            {
                maszyna.Skanuj("Faktura"); // Tutaj stara drukarka "wybuchnie"
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BŁĄD: {ex.Message}");
            }
        }
    }
}