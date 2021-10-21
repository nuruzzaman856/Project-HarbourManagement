using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Dock Management
    /// </summary>
    public class BerthManagement
    {
        public static int refusedFromDock = 0;
        public static int dockedCount = 0;
        public static int TotalDayCount = 0;
        public static bool addBoatsToDock = true;
        public static List<Boat> _parkedBoats = new List<Boat>();
        public static Dictionary<int, string> harbour = InitHarbour();
        public static double emptyParking = harbour.Count;
        public static void ParkBoats()
        {
            if (File.Exists(FileSystem.filePath) && FileSystem.filePath.Length > 0)
            {
                _parkedBoats = FileSystem.ReadBoatInfoFromFile().Select(x => x).ToList();
                _parkedBoats.ForEach(x =>
                {
                    var parkingPlaces = x.ParkingPlace.Split("-").ToList();
                    parkingPlaces.ForEach(p =>
                    {
                        if (int.TryParse(p, out var parkingIndex))
                        {
                            harbour[parkingIndex] = x.IdentityNumber;
                        }
                    });
                });
                DisplayDockInformation.DisplayInfo(_parkedBoats);

            }
            while (addBoatsToDock == true)
            {
                var WaitingBoats = ArrivedBoatsInDock.RandomBoats();
                DisplayDockInformation.WaitingBoatsInHarbourToPark(WaitingBoats);
                var key = Console.ReadKey();
                if (key.KeyChar == 'a')
                {
                    foreach (var boat in WaitingBoats)
                    {
                        switch (boat)
                        {
                            case RowingBoat rowingBoat:
                                if (emptyParking >= rowingBoat.RequiredParkingPlace)
                                {
                                    ParkRowingBoat(rowingBoat);
                                }
                                else
                                {
                                    DisplayDockInformation.CouldNotParkMassage(rowingBoat);
                                }

                                break;
                            case MotorBoat motorBoat:
                                if (emptyParking > motorBoat.RequiredParkingPlace)
                                {
                                    ParkMotorBoat(motorBoat);
                                }
                                else
                                {
                                    DisplayDockInformation.CouldNotParkMassage(motorBoat);
                                }
                                break;
                            case SailBoat sailBoat:
                                if (emptyParking >= sailBoat.RequiredParkingPlace)
                                {
                                    ParkSailBoat(sailBoat);
                                }
                                else
                                {
                                    DisplayDockInformation.CouldNotParkMassage(sailBoat);
                                }
                                break;
                            case CargoShip cargoShip:
                                if (emptyParking >= cargoShip.RequiredParkingPlace)
                                {
                                    ParkCargoShip(cargoShip);
                                }
                                else
                                {
                                    DisplayDockInformation.CouldNotParkMassage(cargoShip);
                                }
                                break;
                            default:
                                break;
                        }
                        refusedFromDock = WaitingBoats.Count - dockedCount;
                    }
                    DisplayDockInformation.DisplayInfo(_parkedBoats);
                    NextDay();
                }
                else if (key.KeyChar == 'q')
                {
                    FileSystem.CreatSpreadsheet();
                    Console.WriteLine();
                    Environment.Exit(0);
                }
                else if (key.KeyChar == 'n')
                {
                    NextDay();
                }
            }

        }
        #region Nextday operation
        private static void NextDay()
        {
            refusedFromDock = 0;
            dockedCount = 0;
            Console.WriteLine("....................................................................................................................................");
            Console.WriteLine("Press [N] next Day and [Q] to Quit ");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'n')
            {
                TotalDayCount += 1;
                var parkedBoatsCopy = new List<Boat>();
                _parkedBoats.ForEach(b =>
                {
                    b.DaysCount -= 1;
                    parkedBoatsCopy.Add(b);
                });

                foreach (var parkBoat in _parkedBoats)
                {
                    switch (parkBoat)
                    {
                        case RowingBoat rowingBoat:
                            if (rowingBoat.DaysCount == 0)
                                parkedBoatsCopy = RemoveRowingBoatFromParking(rowingBoat, parkedBoatsCopy);
                            break;
                        case MotorBoat motorBoat:
                            if (motorBoat.DaysCount == 0)
                                parkedBoatsCopy = RemoveMotorBoatFromParking(motorBoat, parkedBoatsCopy);
                            break;
                        case SailBoat sailBoat:
                            if (sailBoat.DaysCount == 0)
                                parkedBoatsCopy = RemoveSailBoatFromParking(sailBoat, parkedBoatsCopy);
                            break;
                        case CargoShip cargoShip:
                            if (cargoShip.DaysCount == 0)
                                parkedBoatsCopy = RemoveCargoShipFromParking(cargoShip, parkedBoatsCopy);
                            break;
                        default:
                            break;
                    }
                }
                _parkedBoats = parkedBoatsCopy.Where(x => x.DaysCount > 0).Select(x => x).ToList();

            }
            else if (key.KeyChar == 'q')
            {
                FileSystem.CreatSpreadsheet();
                addBoatsToDock = false;
            }
            DisplayDockInformation.DisplayInfo(_parkedBoats);
        }
        private static void ParkRowingBoat(RowingBoat boat)
        {
            for (int i = 1; i <= harbour.Count; i++)
            {
                if (harbour[i] == null)
                {
                    harbour[i] = boat.IdentityNumber;
                    emptyParking -= boat.RequiredParkingPlace;
                    boat.ParkingPlace = $"{ i }";
                    _parkedBoats.Add(boat);
                    dockedCount += 1;
                    break;
                }
                else if (harbour[i] != null && harbour[i].StartsWith("R") && harbour[i].Split(", ").Length == 1)
                {
                    harbour[i] = $"{harbour[i]}, {boat.IdentityNumber}";
                    emptyParking -= boat.RequiredParkingPlace;
                    boat.ParkingPlace = $"{ i }";
                    _parkedBoats.Add(boat);
                    dockedCount += 1;
                    break;
                }
            }
        }
        private static void ParkMotorBoat(MotorBoat boat)
        {
            for (int i = 1; i <= harbour.Count; i++)
            {
                if (harbour[i] == null)
                {
                    harbour[i] = boat.IdentityNumber;
                    boat.ParkingPlace = $"{ i }";
                    _parkedBoats.Add(boat);
                    emptyParking -= boat.RequiredParkingPlace;
                    dockedCount += 1;
                    break;
                }
            }
        }
        private static void ParkSailBoat(SailBoat boat)
        {
            for (int i = 1; i <= harbour.Count; i++)
            {
                if (harbour[i] == null)
                {
                    if (harbour.ContainsKey(i + 1))
                    {
                        harbour[i] = boat.IdentityNumber;
                        harbour[i + 1] = boat.IdentityNumber;
                        boat.ParkingPlace = $"{ i }-{ i + 1}";
                        _parkedBoats.Add(boat);
                        emptyParking -= boat.RequiredParkingPlace;
                        dockedCount += 1;
                        break;
                    }
                }
            }
        }
        private static void ParkCargoShip(CargoShip boat)
        {

            for (int i = 1; i <= harbour.Count; i++)
            {
                if (harbour[i] == null)
                {
                    if (harbour.ContainsKey(i + 1) && harbour.ContainsKey(i + 2) && harbour.ContainsKey(i + 3))
                    {
                        harbour[i] = boat.IdentityNumber;
                        harbour[i + 1] = boat.IdentityNumber;
                        harbour[i + 2] = boat.IdentityNumber;
                        harbour[i + 3] = boat.IdentityNumber;
                        boat.ParkingPlace = $"{ i }-{ i + 1}-{ i + 2}-{ i + 3}";
                        _parkedBoats.Add(boat);
                        emptyParking -= boat.RequiredParkingPlace;
                        dockedCount += 1;
                        break;
                    }
                }
            }
        }


        private static List<Boat> RemoveRowingBoatFromParking(RowingBoat rowingBoat, List<Boat> parkedBoats)
        {
            var (parkingPlaceHasValue, parkingPlace) = ParkingPlaceHasFound(rowingBoat);
            if (parkingPlaceHasValue)
            {
                var parkedRowingBoatIds = harbour[parkingPlace.Key].Split(", ").ToList();
                if (parkedRowingBoatIds.Count == 1)
                {
                    harbour[parkingPlace.Key] = null;
                    parkedBoats.Remove(rowingBoat);
                    emptyParking += .5;
                }
                if (parkedRowingBoatIds.Count == 2)
                {
                    parkedRowingBoatIds.Remove(rowingBoat.IdentityNumber);
                    parkedBoats.Remove(rowingBoat);
                    emptyParking += .5;
                    harbour[parkingPlace.Key] = string.Join(", ", parkedRowingBoatIds);
                }
            }
            return parkedBoats;
        }

        private static List<Boat> RemoveMotorBoatFromParking(MotorBoat motorBoat, List<Boat> parkedBoats)
        {
            var (parkingPlaceHasValue, parkingPlace) = ParkingPlaceHasFound(motorBoat);
            if (parkingPlaceHasValue)
            {
                harbour[parkingPlace.Key] = null;
                parkedBoats.Remove(motorBoat);
                emptyParking += 1;
            }
            return parkedBoats;
        }
        private static List<Boat> RemoveSailBoatFromParking(SailBoat sailBoat, List<Boat> parkedBoats)
        {
            var (parkingPlaceHasValue, parkingPlace) = ParkingPlaceHasFound(sailBoat);
            if (parkingPlaceHasValue)
            {
                harbour[parkingPlace.Key] = null;
                harbour[parkingPlace.Key + 1] = null;
                parkedBoats.Remove(sailBoat);
                emptyParking += 2;
            }
            return parkedBoats;
        }
        private static List<Boat> RemoveCargoShipFromParking(CargoShip cargoShip, List<Boat> parkedBoats)
        {
            var (parkingPlaceHasValue, parkingPlace) = ParkingPlaceHasFound(cargoShip);
            if (parkingPlaceHasValue)
            {
                harbour[parkingPlace.Key] = null;
                harbour[parkingPlace.Key + 1] = null;
                harbour[parkingPlace.Key + 2] = null;
                harbour[parkingPlace.Key + 3] = null;
                parkedBoats.Remove(cargoShip);
                emptyParking += 4;
            }
            return parkedBoats;
        }

        private static (bool, KeyValuePair<int, string>) ParkingPlaceHasFound(Boat boat)
        {
            var parkingPlace = harbour.FirstOrDefault(x => x.Value != null && x.Value.Contains(boat.IdentityNumber));
            var parkingAndValueFound = harbour.ContainsKey(parkingPlace.Key) && harbour[parkingPlace.Key] != null;

            return (parkingAndValueFound, parkingPlace);
        }
        #endregion
        private static Dictionary<int, string> InitHarbour()
        {
            var _harbour = new Dictionary<int, string>();
            for (int i = 1; i <= 64; i++)
            {
                _harbour.Add(i, null);
            }
            return _harbour;
        }

    }
}