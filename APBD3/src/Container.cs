using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.src
{
    public abstract class Container
    {
        private static int Counter = 0;

        public string SerialNumber { get; private set; }
        public double Height { get; private set; } // cm
        public double TareWeight { get; private set; } // kg
        public double Depth { get; private set; } // cm
        public double MaxPayload { get; private set; } // kg
        public double CargoMass { get; protected set; } // kg

        protected Container(string typeLetter, double height, double tareWeight, double depth, double maxPayload)
        {
            if (height <= 0 || tareWeight <= 0 || depth <= 0 || maxPayload <= 0)
                throw new ArgumentException("Container dimensions and weights must be positive.");

            Counter++;
            SerialNumber = $"KON-{typeLetter}-{Counter}";
            Height = height;
            TareWeight = tareWeight;
            Depth = depth;
            MaxPayload = maxPayload;
            CargoMass = 0;
        }

        public virtual void LoadCargo(double mass)
        {
            if (mass < 0)
                throw new ArgumentException("Cargo mass cannot be negative.");
            if (mass > MaxPayload)
                throw new OverfillException($"Cannot load {mass} kg into {SerialNumber}. Max payload is {MaxPayload} kg.");
            CargoMass = mass;
        }

        public virtual void EmptyCargo()
        {
            CargoMass = 0;
        }

        public double TotalWeight => TareWeight + CargoMass;

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Container {SerialNumber}:");
            Console.WriteLine($"  Height: {Height} cm, Tare Weight: {TareWeight} kg, Depth: {Depth} cm");
            Console.WriteLine($"  Max Payload: {MaxPayload} kg, Cargo Mass: {CargoMass} kg, Total Weight: {TotalWeight} kg");
        }
    }
}
