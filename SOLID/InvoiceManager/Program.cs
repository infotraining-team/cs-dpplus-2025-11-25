using System;
using System.ComponentModel.DataAnnotations;
using System.IO;


public interface IValidator
{
    public bool isValid(double amount, string email);
}

public interface ISaver
{
    public void SaveInvoice(int id, double amount);
}

public interface INotify
{
    public void Notify(string email, int invoiceId);
}

public class InvoiceValidator : IValidator
{
    public bool isValid(double amount, string email)
    {
        if (amount < 0) return false;
        if (string.IsNullOrEmpty(email) || !email.Contains('@'))
            return false;
        return true;
    }

}

public class InvoiceSaver : ISaver
{
    public void SaveInvoice(int id, double amount)
    {
        try
        {
            string invoiceData = $"ID: {id}, Data: {DateTime.Now}, Kwota: {amount}";
            File.WriteAllText($"invoice_{id}.txt", invoiceData);
            Console.WriteLine($"Faktura {id} została zapisana na dysku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd podczas zapisu pliku: " + ex.Message);
            return;
        }
    }
}

public class InvoiceNotification : INotify
{
    public void Notify(string email, int invoiceId)
    {
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

public class Process(IValidator? validator, ISaver? saver, INotify? notify)
{
    public void ProcessInvoice(int invoiceId, double amount, string address)
    {
        if (validator != null)// 1. Walidacja danych (Logika biznesowa)
            validator.isValid(amount, address);
        // 2. Zapis faktury do pliku
        if (saver != null)
            saver.SaveInvoice(invoiceId, amount);
        // 3. Wysłanie powiadomienia
        if (notify != null)
            notify.Notify(address, invoiceId);
    }
}

public interface IProcessBuilder
{
    void Reset();
    void BuildValidator();
    void BuildSaver();
    void BuildNotifier();
    Process BuildProcess();
}

public class ProcessBuilderEU : IProcessBuilder
{
    private IValidator _validator = null;
    private ISaver _saver = null;
    private INotify _notifier = null;

    public void Reset()
    {
        _validator = null;
        _saver = null;
        _notifier = null;
    }
    public void BuildValidator()
    {
        _validator = new InvoiceValidator();
    }

    public void BuildSaver()
    {
        _saver = new InvoiceSaver();
    }

    public void BuildNotifier()
    {
        _notifier = new InvoiceNotification();
    }

    public Process BuildProcess()
    {
        return new Process(_validator, _saver, _notifier);
    }
}

public class InvoiceManager
{
    private IProcessBuilder _builder;
    public InvoiceManager(IProcessBuilder builder) 
    {
        _builder = builder;
    }

    public void PartialProcess()
    {
        _builder.Reset();
        _builder.BuildNotifier();
    }

    public void FullProcess()
    {
        _builder.Reset();
        _builder.BuildValidator();
        _builder.BuildSaver();
        _builder.BuildNotifier();
    }
}

public class Program
{
    public static void Main()
    {
        var builder = new ProcessBuilderEU();
        var manager = new InvoiceManager(builder); // Director
        manager.PartialProcess();
        var process = builder.BuildProcess();
        process.ProcessInvoice(102, -100.00, "bezwalidacji-test.pl");
        manager.FullProcess();
        var fullProcess = builder.BuildProcess();
        fullProcess.ProcessInvoice(101, 100.00, "test@test.pl");
    }
}