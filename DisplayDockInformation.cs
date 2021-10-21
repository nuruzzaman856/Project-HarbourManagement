using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HarbourManagement
{
    /// <summary>
    /// Display all the information Of the dock
    /// </summary>
    class DisplayDockInformation
    {
        private static Dictionary<int, string> harbour = BerthManagement.harbour;
        public static void DisplayInfo(List<Boat> _parkedBoats)
        {
            Console.WriteLine("Dock[N]  \tBoatType \t IdentityNumber \t Weight \t MaxSpeed \t  Others \t  \tDaysIntheDock");
            Console.WriteLine("...........................................................................................................................................................................");
            Console.WriteLine();
            for (int i = 1; i <= harbour.Count;)
            {
                if (harbour[i] == null)
                {
                    Console.WriteLine($"{i}  -------Empty-----------------------------------------------------------------------------------------------------");
                    i += 1;
                }
                else
                {
                    var keys = new List<int>();
                    var ids = harbour[i].Split(", ").ToList();
                    ids.ForEach(x =>
                    {
                        var boat = _parkedBoats.FirstOrDefault(b => b.IdentityNumber == x.Trim());
                        keys = harbour.Where(a => a.Value != null && a.Value.Contains(boat.IdentityNumber)).Select(d => d.Key).ToList();
                        PrintBoatInfo(boat);
                    });
                    i += keys.Count();

                }
            }

            Console.WriteLine();
            Console.WriteLine("...........................................................................................................................................................................");

            var TotalBoats = _parkedBoats
                            .Where(x => x != null)
                            .GroupBy(t => t.BoatType);

            foreach (var boats in TotalBoats)
            {
                if (boats.Key != null)
                {
                    Console.Write($"{boats.Key}: {boats.Count()}\t");
                }

            }

            var Weights = _parkedBoats.Sum(w => w.Weight);

            Console.Write($"Total Weights:{Weights}\t");

            if (_parkedBoats.Count > 0)
            {
                var AverageSpeed = _parkedBoats.Sum(a => a.MaximumSpeed);
                Console.Write($"AverageSpeed:{AverageSpeed / _parkedBoats.Count}\t ");
            }
            Console.Write($"Total Parking: {harbour.Count}\t");
            Console.Write($"EmptyParking: {BerthManagement.emptyParking}\t ");
            Console.Write($" RefusedBoats Today: {BerthManagement.refusedFromDock}\t");
            Console.WriteLine();
            Console.WriteLine("...........................................................................................................................................................................");

        }
        public static void CouldNotParkMassage(Boat boat)
        {
            Console.WriteLine($"There is no parking place empty for {boat.BoatType} with ID {boat.IdentityNumber}.Thank You, Come again.");
            Console.WriteLine();
            Console.WriteLine("...........................................................................................................................................................................");
            Thread.Sleep(1000);
        }
        public static void WaitingBoatsInHarbourToPark(List<Boat> boats)
        {
            int Count = 1;
            Console.WriteLine("...........................................................................................................................................................................");
            Console.Write($"Waiting Boats Today:");
            foreach (var boat in boats)
            {
                Console.Write($"[{Count}].{boat.BoatType}   ");
                Count++;
            }
            Console.WriteLine();
            Console.WriteLine("...........................................................................................................................................................................");
            Console.WriteLine($"Press [A] to Add and [Q] to Quit OR [N] For Nextday. ! Day : {BerthManagement.TotalDayCount} in Dock !");
            Console.WriteLine("...........................................................................................................................................................................");
            Console.WriteLine();

        }
        public static void PrintBoatInfo(Boat boat)
        {
            switch (boat.BoatType)
            {
                case "RowingBoat":
                    Console.WriteLine($"{boat.ParkingPlace}\t\t{boat.GetType().Name}\t\t{boat.IdentityNumber}\t\t{boat.Weight} kg\t\t{boat.MaximumSpeed}km/h\tNumberOfHorsepower:{((RowingBoat)boat).MaximumNumberOfPassengers} hp \t{boat.DaysCount} Day");
                    break;
                case "MotorBoat":
                    Console.WriteLine($"{boat.ParkingPlace} \t\t{boat.GetType().Name}\t\t{boat.IdentityNumber}\t\t{boat.Weight} kg\t\t{boat.MaximumSpeed} km/h\tNumberOfHorsepower:{((MotorBoat)boat).NumberOfHorsepower} hp \t{boat.DaysCount} Day");
                    break;
                case "SailBoat":
                    Console.WriteLine($"{boat.ParkingPlace}\t\t{boat.GetType().Name}\t\t{boat.IdentityNumber}\t\t{boat.Weight} kg\t\t{boat.MaximumSpeed} km/h\tBoatLength:{((SailBoat)boat).BoatLength}ft  \t{boat.DaysCount} Day");
                    break;
                case "CargoShip":
                    Console.WriteLine($"{boat.ParkingPlace}\t\t{boat.GetType().Name}\t  {boat.IdentityNumber}\t  {boat.Weight} kg\t  {boat.MaximumSpeed} km/h\tBoatLength:{((CargoShip)boat).NumberOfContainers}\t{boat.DaysCount} Day");
                    break;
                default:
                    break;
            }
        }



    }
}
