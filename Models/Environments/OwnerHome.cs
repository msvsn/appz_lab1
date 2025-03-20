using System;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.Models.Environments
{
    public class OwnerHome : LivingEnvironment, ICleanable
    {
        private readonly string _ownerName;
        public override string Name => $"Дім хазяїна {_ownerName}";
        public override bool NeedsCleaning => true;
        public DateTime LastCleaningTime { get; set; }
        public bool IsCleanEnoughForHappiness => (DateTime.Now - LastCleaningTime).TotalHours <= 24.0;

        public OwnerHome(string ownerName)
        {
            _ownerName = ownerName;
            LastCleaningTime = DateTime.Now;
        }

        public bool Clean()
        {
            LastCleaningTime = DateTime.Now;
            return true;
        }
    }
}