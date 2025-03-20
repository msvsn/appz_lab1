using System;
using System.Threading;
using APPZ_lab1_v6.Models.Interfaces;

namespace APPZ_lab1_v6.Services
{
    public class GameTime : IGameTime
    {
        public DateTime CurrentTime { get; private set; }
        public double GameSpeedMinutesPerDay { get; private set; }
        public bool IsRunning { get; private set; }
        public event EventHandler<DateTime> HourPassed;
        public event EventHandler<DateTime> DayPassed;
        private Thread _timerThread;

        public GameTime(double gameSpeedMinutesPerDay = 10.0)
        {
            CurrentTime = DateTime.Now;
            GameSpeedMinutesPerDay = gameSpeedMinutesPerDay;
            IsRunning = false;
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            _timerThread = new Thread(RunClock);
            _timerThread.IsBackground = true;
            _timerThread.Start();
        }

        public void Stop() => IsRunning = false;

        private void RunClock()
        {
            DateTime lastHour = CurrentTime;
            DateTime lastDay = CurrentTime.Date;

            while (IsRunning)
            {
                double gameSecondsPerRealSecond = (24.0 * 60.0 * 60.0) / (GameSpeedMinutesPerDay * 60.0);
                CurrentTime = CurrentTime.AddSeconds(gameSecondsPerRealSecond);

                if (CurrentTime.Hour != lastHour.Hour || CurrentTime.Day != lastHour.Day)
                {
                    lastHour = new DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day, CurrentTime.Hour, 0, 0);
                    HourPassed?.Invoke(this, CurrentTime);
                }

                if (CurrentTime.Date != lastDay)
                {
                    lastDay = CurrentTime.Date;
                    DayPassed?.Invoke(this, CurrentTime);
                }

                Thread.Sleep(1000);
            }
        }

        public double GetGameHoursDifference(DateTime startTime, DateTime endTime) => (endTime - startTime).TotalHours;
    }
}