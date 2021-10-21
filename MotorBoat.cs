using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Motor Boat Class inherited from Boat class
    /// </summary>
    class MotorBoat : Boat
    {
        public int NumberOfHorsepower { get; set; }
        public double RequiredParkingPlace => 1;
        public MotorBoat(string boatType, string identityNumber, int weight, int maximumSpeed, int daysCout, int numberOfHorsepower) :
            base(boatType, identityNumber, weight, maximumSpeed, daysCout)
        {
            NumberOfHorsepower = numberOfHorsepower;
        }

    }
}
