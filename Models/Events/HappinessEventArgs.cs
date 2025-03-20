using System;

namespace APPZ_lab1_v6.Models.Events
{
    public class HappinessEventArgs : EventArgs
    {
        public string AnimalName { get; }
        public bool IsHappy { get; }
        public string EnvironmentName { get; }
        public HappinessEventArgs(string animalName, bool isHappy, string environmentName)
        {
            AnimalName = animalName;
            IsHappy = isHappy;
            EnvironmentName = environmentName;
        }
    }
}