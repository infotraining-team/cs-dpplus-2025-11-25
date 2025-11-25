using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP.Creational.Builder.Example
{
    public class Car
    {
        internal string Engine { get; set; }
        internal string Gearbox { get; set; }
        internal int AirbagsCount { get; set; }
        internal string Aircondition { get; set; }
        internal List<string> Wheels { get; set; } = new List<string>();

        public override string ToString()
        {
            StringBuilder description = new StringBuilder();
            description.Append(string.Format("Car(engine = {0}, gearbox = {1}, airbags_count = {2}, aircondition = {3}, wheels = [ ",
                Engine, Gearbox, AirbagsCount, (string.IsNullOrEmpty(Aircondition)) ? "None" : Aircondition));

            foreach (var wheel in Wheels)
            {
                description.Append(wheel).Append(" ");
            }

            description.Append("])");

            return description.ToString();
        }
    }

    public interface ICarBuilder
    {
        ICarBuilder BuildEngine();
        ICarBuilder BuildGearbox();
        ICarBuilder BuildAirbags();
        ICarBuilder BuildAircondition();
        ICarBuilder BuildWheel();
    }

    public abstract class CarBuilder : ICarBuilder
    {
        protected Car Car { get; set; }

        protected CarBuilder()
        {
            Car = new Car();
        }

        public abstract ICarBuilder BuildEngine();
        public abstract ICarBuilder BuildGearbox();
        public abstract ICarBuilder BuildAirbags();
        public abstract ICarBuilder BuildAircondition();
        public abstract ICarBuilder BuildWheel();

        public Car GetCar()
        {
            return Car;
        }
    }

    public class EconomyCarBuilder : CarBuilder
    {
        public override ICarBuilder BuildEngine()
        {
            Car.Engine = "petrol 1.1";
            return this;
        }

        public override ICarBuilder BuildGearbox()
        {
            Car.Gearbox = "manual 5";
            return this;
        }

        public override ICarBuilder BuildAirbags()
        {
            Car.AirbagsCount = 1;
            return this;
        }

        public override ICarBuilder BuildAircondition()
        {
            return this;
        }

        public override ICarBuilder BuildWheel()
        {
            Car.Wheels.Add("steel rims 14");
            return this;
        }
    }


    public class Director
    {
        public ICarBuilder CarBuilder { get; set; }

        public Director(ICarBuilder carBuilder)
        {
            CarBuilder = carBuilder;
        }

        public void Construct()
        {
            CarBuilder.BuildEngine();
            CarBuilder.BuildGearbox();
            CarBuilder.BuildAirbags();
            CarBuilder.BuildAircondition();

            for (int i = 0; i < 4; ++i)
                CarBuilder.BuildWheel();
        }
    }

    class Program
    {
        public static void Main()
        {
            EconomyCarBuilder economyCarBuilder = new EconomyCarBuilder();

            Director director = new Director(economyCarBuilder);
            director.Construct();
            Car car = economyCarBuilder.GetCar();

            Console.WriteLine(car.ToString());
        }
    }
}