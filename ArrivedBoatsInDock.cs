using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourManagement
{
    class ArrivedBoatsInDock
    {

        public enum BoatType { RowingBoat = 1, MotorBoat, SailBoat, CargoShip }

        /// <summary>
        ///     Retun 5 randomm Boats
        /// </summary>  
        /// <returns>List<Boat></returns>
        public static List<Boat> RandomBoats()
        {
            List<Boat> randomBoats = new List<Boat>();
            for (int i = 1; i < 6; i++)
            {
                var boatType = new Random();
                var randomBoat = (BoatType)boatType.Next(1, 5);
                switch (randomBoat)
                {
                    case BoatType.RowingBoat:
                        randomBoats.Add(new RowingBoat("RowingBoat", $"R-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(100, 300), Utilities.GetRandomNumber(1, 3), 1, Utilities.GetRandomNumber(2, 6)));
                        break;
                    case BoatType.MotorBoat:
                        randomBoats.Add(new MotorBoat("MotorBoat", $"M-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(200, 3000), Utilities.GetRandomNumber(20, 60), 3, Utilities.GetRandomNumber(100, 1000)));
                        break;
                    case BoatType.SailBoat:
                        randomBoats.Add(new SailBoat("SailBoat", $"S-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(1000, 6000), Utilities.GetRandomNumber(6, 12), 4, Utilities.GetRandomNumber(10, 60)));
                        break;
                    case BoatType.CargoShip:
                        randomBoats.Add(new CargoShip("CargoShip", $"C-{Utilities.GetRandomString(5)}", Utilities.GetRandomNumber(3000, 20000), Utilities.GetRandomNumber(3, 20), 6, Utilities.GetRandomNumber(50, 500)));
                        break;
                    default:
                        break;
                }
            }

            return randomBoats;
        }
    }
}
