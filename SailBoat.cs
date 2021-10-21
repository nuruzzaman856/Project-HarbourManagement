using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// SAil Boat Class inherited from Boat class
    /// </summary>
    class SailBoat : Boat
    {
        public int BoatLength { get; set; }
        public double RequiredParkingPlace => 2;

        public SailBoat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int boatLength) : base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            BoatLength = boatLength;
        }
    }
}
