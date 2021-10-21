using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Boat Main Class
    /// </summary>
    public class Boat
    {
        public string BoatType { get; set; }

        public string IdentityNumber { get; set; }
        public int Weight { get; set; }
        public int MaximumSpeed { get; set; }
        public int DaysCount { get; set; }

        public string ParkingPlace { get; set; }
        public Boat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, string parking = "")
        {
            BoatType = boatType;
            IdentityNumber = identityNumber;
            Weight = weight;
            MaximumSpeed = maximumSpeed;
            DaysCount = daysCout;
            ParkingPlace = parking;
        }



    }
}
