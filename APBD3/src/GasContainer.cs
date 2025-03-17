using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.src
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; private set; } // in atmospheres

        public GasContainer(double height, double tareWeight, double depth, double maxPayload, double pressure)
            : base("G", height, tareWeight, depth, maxPayload)
        {
            if (pressure <= 0)
                throw new ArgumentException("Pressure must be positive.");
            Pressure = pressure;
        }

        public override void EmptyCargo()
        {
            CargoMass *= 0.05; // Leave 5% of cargo
        }

        public void SendNotification(string message)
        {
            Console.WriteLine($"Hazard Notification for {SerialNumber}: {message}");
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"  Type: Gas, Pressure: {Pressure} atm");
        }
    }
}
