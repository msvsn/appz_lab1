using System;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface ICleanable
    {
        DateTime LastCleaningTime { get; set; }
    }
}