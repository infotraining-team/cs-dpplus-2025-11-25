using System;

public interface IAttackStrategy
{
    void Attack(string target);
}


public class StabAttack : IAttackStrategy
{
    public void Attack(string target)
    {
        Console.WriteLine($"Stabbed the {target}");
    }
}
// BAD CODE: Violation of OCP (Open/Closed Principle) and SRP (Single Responsibility Principle)
public class Monster
{
    private IAttackStrategy _attackStrategy;
    public string Type { get; set; }
    public int Health { get; set; }

    public Monster(string type, int health, IAttackStrategy strategy)
    {
        _attackStrategy = strategy;
        Type = type;
        Health = health;
    }

    public void Attack(string target)
    {
        // PROBLEM 1: Hardcoded logic inside the class.
        // If we want to add "Wizard", we have to modify this method.
        if (Type == "Goblin")
        {
            Console.WriteLine($"Goblin stabs {target} with a rusty knife for 5 damage!");
        }
        else if (Type == "Dragon")
        {
            Console.WriteLine($"Dragon breathes fire on {target} for 50 damage! It's super effective.");
        }
        else if (Type == "Zombie")
        {
            Console.WriteLine($"Zombie bites {target} for 10 damage. {target} is infected.");
        }

        _attackStrategy.Attack(target);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        // PROBLEM 2: Direct dependency on Console (UI). 
        // We cannot use this class in a WPF app or Unit Tests easily.
        Console.WriteLine($"Monster {Type} took {amount} damage. HP left: {Health}");

        if (Health <= 0)
        {
            Console.WriteLine($"Monster {Type} has died!");
        }
    }
}

public static class MonsterFactory
{
    public static Monster CreateGoblin()
    {
        return new Monster("Goblin", 20)
    }
}

public class Program
{
    public static void Main()
    {
        var goblin = new Monster("Goblin", 20);
        goblin.Attack("Hero");
        goblin.TakeDamage(25);
    }
}

/*

Wzorzec Strategy: Wyciągnij logikę ataku (Attack) do interfejsu IAttackStrategy.
Stwórz konkretne klasy (StabAttack, FireBreathAttack).
Klasa Monster powinna przyjmować strategię w konstruktorze.

Wzorzec Observer (C# Events): Usuń Console.WriteLine z metody TakeDamage.
Zamiast tego, zdefiniuj zdarzenie OnDamageTaken (lub OnDeath),
które powiadomi subskrybentów o zmianie stanu.

Wzorzec Factory Method: Stwórz klasę MonsterFactory,
która ukryje tworzenie potworów (np. CreateGoblin())
i automatycznie przypisze im odpowiednią strategię oraz statystyki. 

*/
