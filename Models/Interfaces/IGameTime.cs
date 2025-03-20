using System;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IGameTime
    {
        DateTime CurrentTime { get; }
        double GameSpeedMinutesPerDay { get; }
        bool IsRunning { get; }
        event EventHandler<DateTime> HourPassed;
        event EventHandler<DateTime> DayPassed;
        void Start();
        void Stop();
        double GetGameHoursDifference(DateTime startTime, DateTime endTime);
    }
}