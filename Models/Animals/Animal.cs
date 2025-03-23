using System;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Services;
using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Models.Events;

namespace APPZ_lab1_v6.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; }
        public ILivingEnvironment LivingEnvironment { get; set; }
        public Eyes Eyes { get; set; }
        public Legs Legs { get; set; }
        public DateTime LastFeedingTime { get; set; }
        public abstract int MealsPerDay { get; }
        public bool IsAlive { get; set; } = true;
        public int FeedingsToday { get; set; }
        public DateTime LastFeedingCountDate { get; set; }
        public DateTime LastGameTimeCheck { get; set; }

        public event EventHandler HungryStateChanged;
        public event EventHandler HappinessStateChanged;
        public event EventHandler DeathStateChanged;

        protected Animal(string name, int age, Eyes eyes, Legs legs)
        {
            Name = name;
            Age = age;
            BirthDate = DateTime.Now.AddDays(-age * 365);
            Eyes = eyes;
            Legs = legs;
            LastFeedingTime = DateTime.Now;
            LastFeedingCountDate = DateTime.Now.Date;
            LastGameTimeCheck = DateTime.Now;
        }

        public virtual bool Sing() => false;
        public virtual bool Walk() => IsAlive;
        public virtual bool Run() => false;
        public virtual bool Fly() => false;
        public virtual bool Crawl() => IsAlive;

        public virtual void OnHungryStateChanged(bool isHungry, double hoursSinceFeeding) => 
            HungryStateChanged?.Invoke(this, new HungryEventArgs(Name, isHungry, hoursSinceFeeding));
        public virtual void OnHappinessStateChanged(bool isHappy, string environmentName) => 
            HappinessStateChanged?.Invoke(this, new HappinessEventArgs(Name, isHappy, environmentName));
        public virtual void OnDeathStateChanged(double hoursWithoutFood) => 
            DeathStateChanged?.Invoke(this, new DeathEventArgs(Name, hoursWithoutFood));
    }
}