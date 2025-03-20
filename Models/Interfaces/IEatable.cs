using System;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IEatable
    {
        DateTime LastFeedingTime { get; set; }
        int MealsPerDay { get; }
    }
}