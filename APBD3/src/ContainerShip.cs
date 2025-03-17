using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.src
{
    public class ContainerShip
    {
        public double MaxSpeed { get; private set; } // knots
        public int MaxContainerNum { get; private set; }
        public double MaxWeightTons { get; private set; } // tons
        private readonly List<Container> Containers;

        public ContainerShip(double maxSpeed, int maxContainerNum, double maxWeightTons)
        {
            if (maxSpeed <= 0 || maxContainerNum <= 0 || maxWeightTons <= 0)
                throw new ArgumentException("Ship parameters must be positive.");
            MaxSpeed = maxSpeed;
            MaxContainerNum = maxContainerNum;
            MaxWeightTons = maxWeightTons;
            Containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainerNum)
                throw new InvalidOperationException("Maximum number of containers reached.");
            double newTotalWeightKg = Containers.Sum(c => c.TotalWeight) + container.TotalWeight;
            if (newTotalWeightKg > MaxWeightTons * 1000)
                throw new InvalidOperationException("Maximum weight capacity exceeded.");
            Containers.Add(container);
        }

        public void LoadContainers(List<Container> containers)
        {
            if (Containers.Count + containers.Count > MaxContainerNum)
                throw new InvalidOperationException("Maximum number of containers would be exceeded.");
            double newTotalWeightKg = Containers.Sum(c => c.TotalWeight) + containers.Sum(c => c.TotalWeight);
            if (newTotalWeightKg > MaxWeightTons * 1000)
                throw new InvalidOperationException("Maximum weight capacity would be exceeded.");
            Containers.AddRange(containers);
        }

        public void RemoveContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
                throw new InvalidOperationException($"Container {serialNumber} not found.");
            Containers.Remove(container);
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
                throw new InvalidOperationException($"Container {serialNumber} not found.");
            container.EmptyCargo();
        }

        public void ReplaceContainer(string oldSerialNumber, Container newContainer)
        {
            int index = Containers.FindIndex(c => c.SerialNumber == oldSerialNumber);
            if (index == -1)
                throw new InvalidOperationException($"Container {oldSerialNumber} not found.");
            double weightDelta = newContainer.TotalWeight - Containers[index].TotalWeight;
            double newTotalWeightKg = Containers.Sum(c => c.TotalWeight) + weightDelta;
            if (newTotalWeightKg > MaxWeightTons * 1000)
                throw new InvalidOperationException("Replacement would exceed weight capacity.");
            Containers[index] = newContainer;
        }

        public void TransferContainer(string serialNumber, ContainerShip otherShip)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
                throw new InvalidOperationException($"Container {serialNumber} not found.");
            otherShip.LoadContainer(container);
            Containers.Remove(container);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Container Ship: Speed={MaxSpeed} knots, Max Containers={MaxContainerNum}, Max Weight={MaxWeightTons} tons");
            Console.WriteLine($"Current Containers: {Containers.Count}, Total Weight: {Containers.Sum(c => c.TotalWeight) / 1000} tons");
            foreach (var container in Containers)
            {
                container.PrintInfo();
            }
        }
    }
}
