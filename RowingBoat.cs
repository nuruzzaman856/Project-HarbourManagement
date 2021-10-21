using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Rowing Boat Class inherited from Boat class
    /// </summary>
    class RowingBoat : Boat
    {
        public int MaximumNumberOfPassengers { get; set; }
        public double RequiredParkingPlace => 0.5;

        public RowingBoat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int maximumNumberOfPassengers)
            : base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            MaximumNumberOfPassengers = maximumNumberOfPassengers;
        }

    }
}
