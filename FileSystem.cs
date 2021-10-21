

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Creat File and read from the file
    /// </summary>
    class FileSystem
    {
        private static List<Boat> _parkedBoats = BerthManagement._parkedBoats;
        private static Dictionary<int, string> harbour = BerthManagement.harbour;
        public const string filePath = @"C:\Visual Stadio\HarbourManagement\DockedBoatsInHarbor.csv";

        public static void CreatSpreadsheet()
        {
            var csv = new StringBuilder();
            for (int i = 0; i < _parkedBoats.Count; i++)
            {
                if (_parkedBoats[i] != null)
                {

                    if (_parkedBoats[i] is RowingBoat rowingBoat)
                    {
                        var dockedBoat = $"{rowingBoat.BoatType},{rowingBoat.IdentityNumber},{rowingBoat.Weight},{rowingBoat.MaximumSpeed},{rowingBoat.DaysCount},{rowingBoat.MaximumNumberOfPassengers},{rowingBoat.ParkingPlace}";
                        csv.AppendLine(dockedBoat);
                    }
                    else if (_parkedBoats[i] is MotorBoat motorBoat)
                    {
                        var dockedBoat = $"{motorBoat.BoatType},{motorBoat.IdentityNumber},{motorBoat.Weight},{motorBoat.MaximumSpeed},{motorBoat.DaysCount},{motorBoat.MaximumSpeed},{motorBoat.ParkingPlace}";
                        csv.AppendLine(dockedBoat);
                    }
                    else if (_parkedBoats[i] is SailBoat sailBoat)
                    {
                        var dockedBoat = $"{sailBoat.BoatType},{sailBoat.IdentityNumber},{sailBoat.Weight},{sailBoat.MaximumSpeed},{sailBoat.DaysCount},{sailBoat.BoatLength},{sailBoat.ParkingPlace}";
                        csv.AppendLine(dockedBoat);
                    }
                    else if (_parkedBoats[i] is CargoShip cargoShip)
                    {
                        var dockedBoat = $"{cargoShip.BoatType},{cargoShip.IdentityNumber},{cargoShip.Weight},{cargoShip.MaximumSpeed},{cargoShip.DaysCount},{cargoShip.NumberOfContainers},{cargoShip.ParkingPlace}";
                        csv.AppendLine(dockedBoat);
                    }
                }
            }
            File.WriteAllText(filePath, csv.ToString());
        }
        public static List<Boat> ReadBoatInfoFromFile()
        {
            var boats = new List<Boat>();

            foreach (string boat in File.ReadLines(filePath, System.Text.Encoding.UTF7))
            {
                char[] delimiterChars = { ' ', ',' };
                string[] boatData = boat.Trim().Split(delimiterChars);
                if (boatData[0] == "RowingBoat")
                {
                    var rowingBoat = new RowingBoat(boatData[0], boatData[1],
                        int.Parse(boatData[2]), int.Parse(boatData[3]), int.Parse(boatData[4]), int.Parse(boatData[5]));
                    rowingBoat.ParkingPlace = boatData[6];
                    BerthManagement.emptyParking -= .5;
                    boats.Add(rowingBoat);
                }
                else if (boatData[0] == "MotorBoat")
                {
                    var motorBoat = new MotorBoat(boatData[0], boatData[1],
                                            int.Parse(boatData[2]), int.Parse(boatData[3]), int.Parse(boatData[4]), int.Parse(boatData[5]));
                    motorBoat.ParkingPlace = boatData[6];
                    BerthManagement.emptyParking -= 1;
                    boats.Add(motorBoat);
                }
                else if (boatData[0] == "SailBoat")
                {
                    var sailBoat = new SailBoat(boatData[0], boatData[1],
                                            int.Parse(boatData[2]), int.Parse(boatData[3]), int.Parse(boatData[4]), int.Parse(boatData[5]));
                    sailBoat.ParkingPlace = boatData[6];
                    BerthManagement.emptyParking -= 2;
                    boats.Add(sailBoat);
                }
                else if (boatData[0] == "CargoShip")
                {
                    var cargoShip = new CargoShip(boatData[0], boatData[1],
                                            int.Parse(boatData[2]), int.Parse(boatData[3]), int.Parse(boatData[4]), int.Parse(boatData[5]));
                    cargoShip.ParkingPlace = boatData[6];
                    BerthManagement.emptyParking -= 4;
                    boats.Add(cargoShip);
                }
            }
            return boats;
        }
    } 
}
