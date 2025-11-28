using System;
using System.Collections.Generic;

// 1. Command Interface
// Declares the execution method and the undo method.
public interface ICommand
{
    void Execute();
    void Undo();
}

// 2. Receiver
// The object that knows how to perform the actual work.
public class Light
{
    public void TurnOn() => Console.WriteLine("Light is ON");
    public void TurnOff() => Console.WriteLine("Light is OFF");
}

// 3. Concrete Commands
// Implements the command by invoking operations on the receiver.
public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }

    public void Undo()
    {
        Console.WriteLine("Undoing light on operation...");
        _light.TurnOff();
    }
}

public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }

    public void Undo()
    {
        Console.WriteLine("Undoing light off operation...");
        _light.TurnOn();
    }
}

// 4. Invoker
// Sends requests to the command. It maintains a history for Undo functionality.
public class RemoteControl
{
    private ICommand _lastCommand;
    // For multiple undo steps, we would use a Stack<ICommand>
    private Stack<ICommand> _commandHistory = new Stack<ICommand>();

    public void PressButton(ICommand command)
    {
        // Execute the command
        command.Execute();

        // Save it to history
        _commandHistory.Push(command);
    }

    public void PressUndo()
    {
        if (_commandHistory.Count > 0)
        {
            var command = _commandHistory.Pop();
            command.Undo();
        }
        else
        {
            Console.WriteLine("Nothing to undo.");
        }
    }
}

// 5. Client Code
public class Program
{
    public static void Main()
    {
        // Setup
        var livingRoomLight = new Light();
        var turnOn = new LightOnCommand(livingRoomLight);
        var turnOff = new LightOffCommand(livingRoomLight);

        var remote = new RemoteControl();

        Console.WriteLine("--- User presses ON ---");
        remote.PressButton(turnOn);

        Console.WriteLine("\n--- User presses OFF ---");
        remote.PressButton(turnOff);

        Console.WriteLine("\n--- User realizes mistake and presses UNDO ---");
        remote.PressUndo(); // Should turn the light back ON

        Console.WriteLine("\n--- User presses UNDO again ---");
        remote.PressUndo(); // Should turn the light OFF
    }
}