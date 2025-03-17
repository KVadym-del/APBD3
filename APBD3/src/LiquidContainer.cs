using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.src
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; private set; }

        public LiquidContainer(double height, double tareWeight, double depth, double maxPayload, bool isHazardous)
            : base("L", height, tareWeight, depth, maxPayload)
        {
            IsHazardous = isHazardous;
        }

        public override void LoadCargo(double mass)
        {
            double allowedLimit = IsHazardous ? 0.5 * MaxPayload : 0.9 * MaxPayload;
            if (mass > allowedLimit)
            {
                SendNotification($"Attempt to load {mass} kg exceeds allowed limit of {allowedLimit} kg for {(IsHazardous ? "hazardous" : "ordinary")} cargo.");
            }
            base.LoadCargo(mass);
        }

        public void SendNotification(string message)
        {
            Console.WriteLine($"Hazard Notification for {SerialNumber}: {message}");
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"  Type: Liquid, Hazardous: {IsHazardous}");
        }
    }
}
