namespace DIP_excersise_start
{
    using System;

    // Moduł Niskiego Poziomu (Szczegół)
    public class ZarowkaLED
    {
        public void Zapal()
        {
            Console.WriteLine("Żarówka: Świecę jasnym światłem!");
        }

        public void Zgas()
        {
            Console.WriteLine("Żarówka: Ciemność...");
        }
    }

    // Moduł Wysokiego Poziomu (Logika sterowania)
    public class PrzelacznikScienny
    {
        // BŁĄD DIP: Bezpośrednia zależność od konkretnej klasy!
        // Przełącznik jest "przyspawany" do żarówki.
        private ZarowkaLED _zarowka;
        private bool _jestWlaczony = false;

        public PrzelacznikScienny()
        {
            _zarowka = new ZarowkaLED(); // Twarda zależność (Tight Coupling)
        }

        public void Przelacz()
        {
            if (_jestWlaczony)
            {
                _zarowka.Zgas();
                _jestWlaczony = false;
            }
            else
            {
                _zarowka.Zapal();
                _jestWlaczony = true;
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var przelacznik = new PrzelacznikScienny();
            przelacznik.Przelacz(); // Włącz
            przelacznik.Przelacz(); // Wyłącz

            // Zadanie: Spróbuj podłączyć tu Wentylator bez psucia klasy PrzelacznikScienny.
            // Spoiler: Nie da się w obecnym kodzie.
        }
    }
}