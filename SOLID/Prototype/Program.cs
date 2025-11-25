
internal class Program
{
    static void Main(string[] args)
    {
        var person1 = new Person();
        person1.name = "Test";
        person1.address.Street = "Bajeczna";

        var person2 = (Person)person1.Clone();
        person1.address.Street = "Kolorowa";

        Console.WriteLine($"Person1: {person1.name}, {person1.address.Street}");
        Console.WriteLine($"Person2: {person2.name}, {person2.address.Street}");
    }
}

class Address
{
    public string Street { get; set; }
}

class Person : ICloneable
{
    public Person()
    {
        address = new Address();

    }

    public string name { get; set; }
    public Address address { get; set; }

    public object Clone()
    {
        Person res = (Person)this.MemberwiseClone();
        res.address = new Address();
        res.address.Street = this.address.Street;
        return res;
    }
}
