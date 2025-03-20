using System;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface ICleanable
    {
        bool Clean();
        DateTime LastCleaningTime { get; set; }
        bool IsCleanEnoughForHappiness { get; }
    }
}