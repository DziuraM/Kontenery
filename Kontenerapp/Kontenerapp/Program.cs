using System;
using System.Collections.Generic;

namespace ContainerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ContainerShip ship = new ContainerShip(maxSpeed: 20, maxContainerCount: 5, maxTotalWeight: 50);

                LiquidContainer liquidContainer = new LiquidContainer(maxCapacity: 10000);
                GasContainer gasContainer = new GasContainer(maxCapacity: 8000, pressure: 2.5);
                ReeferContainer reeferContainer = new ReeferContainer(maxCapacity: 12000, height: 250, depth: 300, ownWeight: 4000);

                liquidContainer.LoadCargo(9000, isHazardous: false);

                try
                {
                    gasContainer.LoadCargo(4500, isHazardous: true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd przy ładowaniu kontenera gazowego: {ex.Message}");
                    gasContainer.LoadCargo(4000, isHazardous: true);
                }

                reeferContainer.LoadCargo(10000, isHazardous: false, productType: "Banany", temperature: 5);

                ship.AddContainer(liquidContainer);
                ship.AddContainer(gasContainer);
                ship.AddContainer(reeferContainer);

                Console.WriteLine("Stan statku po dodaniu kontenerów:");
                ship.PrintShipInfo();

                LiquidContainer newLiquid = new LiquidContainer(maxCapacity: 11000);
                newLiquid.LoadCargo(9900, isHazardous: false); // 90% z 11000 kg
                ship.ReplaceContainer(liquidContainer.SerialNumber, newLiquid);

                Console.WriteLine("\nStan statku po zastąpieniu kontenera płynnego:");
                ship.PrintShipInfo();

                ContainerShip anotherShip = new ContainerShip(maxSpeed: 25, maxContainerCount: 3, maxTotalWeight: 30);
                ship.TransferContainerTo(anotherShip, gasContainer.SerialNumber);

                Console.WriteLine("\nStan po transferze kontenera gazowego:");
                Console.WriteLine("Statek 1:");
                ship.PrintShipInfo();
                Console.WriteLine("Statek 2:");
                anotherShip.PrintShipInfo();

                ship.UnloadContainer(newLiquid.SerialNumber);
                Console.WriteLine("\nStan statku po rozładowaniu kontenera:");
                ship.PrintShipInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
        }
    }
}
