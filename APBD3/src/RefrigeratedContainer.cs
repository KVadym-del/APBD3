using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.src
{
    public enum ProductType
    {
        Bananas,
        Chocolate,
        Fish,
        Meat,
        IceCream,
        Milk // Added for completeness, though typically in liquid containers
    }

    public class RefrigeratedContainer : Container
    {
        private static readonly Dictionary<ProductType, double> RequiredTemperatures = new Dictionary<ProductType, double>
    {
        { ProductType.Bananas, 13.3 },
        { ProductType.Chocolate, 18.0 },
        { ProductType.Fish, -18.0 },
        { ProductType.Meat, -20.0 },
        { ProductType.IceCream, -25.0 },
        { ProductType.Milk, 2.0 }
    };

        public ProductType ProductType { get; private set; }
        public double Temperature { get; private set; } // Celsius

        public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload, ProductType productType, double temperature)
            : base("C", height, tareWeight, depth, maxPayload)
        {
            if (!RequiredTemperatures.ContainsKey(productType))
                throw new ArgumentException("Unsupported product type.");
            if (temperature < RequiredTemperatures[productType])
                throw new ArgumentException($"Temperature {temperature}°C is below the required {RequiredTemperatures[productType]}°C for {productType}.");

            ProductType = productType;
            Temperature = temperature;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"  Type: Refrigerated, Product: {ProductType}, Temperature: {Temperature}°C");
        }
    }
}
