using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Cargo Ship Class inherited from Boat class
    /// </summary>
    class CargoShip : Boat
    {
        public int NumberOfContainers { get; set; }
        public double RequiredParkingPlace => 4;

        public CargoShip(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int numberOfContainers) : base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            NumberOfContainers = numberOfContainers;
        }



    }
}
