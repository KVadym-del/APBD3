# Unfathomable implementation of the ships and cargos using c#

Example:
```cs
using APBD3.src;

class Program
{
    static void Main()
    {
        try
        {
            var ship1 = new ContainerShip(25.0, 5, 500000.0); // 25 knots, 5 containers, 500000.0 tons (for test cases)
            var ship2 = new ContainerShip(20.0, 3, 30.0);

            var liquidMilk = new LiquidContainer(243, 1200, 243, 26000); // Ordinary cargo (milk)
            var liquidFuel = new LiquidContainer(243, 1200, 243, 26000, true);  // Hazardous cargo (fuel)
            var gasHelium = new GasContainer(243, 1200, 243, 26000, 2.0);      // Helium at 2 atm
            var refrigeratedBananas = new RefrigeratedContainer(243, 1200, 243, 26000, ProductType.Bananas, 15.0);

            Console.WriteLine("=== Testing Container Operations ===");
            liquidMilk.LoadCargo(24000); // Exceeds 90% (23400 kg), expect notification
            liquidFuel.LoadCargo(14000); // Exceeds 50% (13000 kg), expect notification
            gasHelium.LoadCargo(20000);
            gasHelium.EmptyCargo(); // Should leave 1000 kg (5% of 20000)
            refrigeratedBananas.LoadCargo(20000);

            liquidMilk.PrintInfo();
            liquidFuel.PrintInfo();
            gasHelium.PrintInfo();
            refrigeratedBananas.PrintInfo();

            Console.WriteLine("\n=== Testing Ship Operations ===");
            ship1.LoadContainer(liquidMilk);
            ship1.LoadContainer(liquidFuel);
            ship1.LoadContainer(gasHelium);
            ship1.LoadContainer(refrigeratedBananas);

            ship1.PrintInfo();

            try
            {
                var extraContainer = new LiquidContainer(243, 1200, 243, 26000, false);
                extraContainer.LoadCargo(10000);
                ship1.LoadContainer(extraContainer); // Should fail (max 5 containers)
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            var newContainer = new RefrigeratedContainer(243, 1200, 243, 26000, ProductType.Chocolate, 20.0);
            newContainer.LoadCargo(15000);
            ship1.ReplaceContainer(liquidMilk.SerialNumber, newContainer);

            ship1.TransferContainer(gasHelium.SerialNumber, ship2);

            ship1.UnloadContainer(liquidFuel.SerialNumber);

            ship1.RemoveContainer(refrigeratedBananas.SerialNumber);

            Console.WriteLine("\n=== Updated Ship 1 Info ===");
            ship1.PrintInfo();
            Console.WriteLine("\n=== Ship 2 Info ===");
            ship2.PrintInfo();

            try
            {
                liquidMilk.LoadCargo(30000); // Exceeds 26000 kg
            }
            catch (OverfillException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
```
