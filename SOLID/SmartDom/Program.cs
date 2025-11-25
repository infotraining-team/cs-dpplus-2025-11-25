namespace DIP_excersise_solution
{
    using System;

    // 1. Abstrakcja (Interfejs)
    public interface IElectricDevice
    {
        void On();
        void Off();
    }

    // 2. Moduły niskiego poziomu (Szczegóły)
    public class LedBulb : IElectricDevice
    {
        public void On() => Console.WriteLine("Bulb: Light on!");
        public void Off() => Console.WriteLine("Buld: Dark!");
    }

    public class Fan : IElectricDevice
    {
        public void On() => Console.WriteLine("Fan: Blows!");
        public void Off() => Console.WriteLine("Fan: Stops");
    }

    // 3. Moduł Wysokiego Poziomu
    // Nie wie, czym steruje. Wie tylko, że to coś ma metody On/Off.
    public class WallSwitch
    {
        private readonly IElectricDevice _device;
        private bool _isOn = false;

        // Wstrzykiwanie zależności (Dependency Injection) umożliwia DIP
        public WallSwitch(IElectricDevice device)
        {
            _device = device;
        }

        public void Switch()
        {
            if (_isOn)
            {
                _device.Off();
                _isOn = false;
            }
            else
            {
                _device.On();
                _isOn = true;
            }
        }
    }

    // 4. Composition Root (Sklejanie całości)
    public class Program
    {
        public static void Main()
        {
            // Scenariusz 1: Światło
            IElectricDevice bulb = new LedBulb();
            var wallSwitch = new WallSwitch(bulb);

            Console.WriteLine("--- Light Test ---");
            wallSwitch.Switch();

            // Scenariusz 2: Fan (Działa bez zmiany kodu przełącznika!)
            IElectricDevice fan = new Fan();
            var fanSwitch = new WallSwitch(fan);

            Console.WriteLine("\n--- Fan Test ---");
            fanSwitch.Switch();
        }
    }
}
