using System;

namespace APPZ_lab1_v6.Models.Events
{
    public class HungryEventArgs : EventArgs
    {
        public string AnimalName { get; }
        public bool IsHungry { get; }
        public double HoursSinceLastFeeding { get; }
        public HungryEventArgs(string animalName, bool isHungry, double hoursSinceFeeding)
        {
            AnimalName = animalName;
            IsHungry = isHungry;
            HoursSinceLastFeeding = hoursSinceFeeding;
        }
    }
}