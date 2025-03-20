using System;

namespace APPZ_lab1_v6.Models.Events
{
    public class DeathEventArgs : EventArgs
    {
        public string AnimalName { get; }
        public double HoursWithoutFood { get; }
        public DeathEventArgs(string animalName, double hoursWithoutFood)
        {
            AnimalName = animalName;
            HoursWithoutFood = hoursWithoutFood;
        }
    }
}