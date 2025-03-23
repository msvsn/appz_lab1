using System;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalStateChanged
    {
        event EventHandler HungryStateChanged;
        event EventHandler HappinessStateChanged;
        event EventHandler DeathStateChanged;
        bool IsAlive { get; set; }
        int FeedingsToday { get; set; }
        DateTime LastFeedingCountDate { get; set; }
        DateTime LastGameTimeCheck { get; set; }
        DateTime LastFeedingTime { get; set; }
        int MealsPerDay { get; }
    }
}