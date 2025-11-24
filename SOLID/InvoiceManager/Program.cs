using System;
using System.IO;

public class InvoiceManager
{
    public void ProcessInvoice(int invoiceId, double amount, string email)
    {
        // 1. Walidacja danych (Logika biznesowa)
        if (amount <= 0)
        {
            Console.WriteLine("Błąd: Kwota faktury musi być dodatnia.");
            return;
        }

        if (string.IsNullOrEmpty(email) || !email.Contains("@"))
        {
            Console.WriteLine("Błąd: Nieprawidłowy adres email.");
            return;
        }

        // 2. Zapis faktury do pliku
        try
        {
            string invoiceData = $"ID: {invoiceId}, Data: {DateTime.Now}, Kwota: {amount}";
            File.WriteAllText($"invoice_{invoiceId}.txt", invoiceData);
            Console.WriteLine($"Faktura {invoiceId} została zapisana na dysku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd podczas zapisu pliku: " + ex.Message);
            return;
        }

        // 3. Wysłanie powiadomienia
        try
        {
            // Symulacja wysyłania maila
            Console.WriteLine($"Wysyłanie emaila do: {email}");
            Console.WriteLine($"Temat: Nowa faktura {invoiceId}");
            Console.WriteLine("Treść: Twoja faktura została wygenerowana.");
            // Tutaj byłby kod SMTP...
        }
        catch (Exception ex)
        {
            Console.WriteLine("Nie udało się wysłać maila: " + ex.Message);
        }
    }
}

public class Program
{
    public static void Main()
    {
        var manager = new InvoiceManager();
        manager.ProcessInvoice(101, 250.00, "klient@firma.pl");
        manager.ProcessInvoice(102, -250.00, "klient-firma.pl");

    }
}