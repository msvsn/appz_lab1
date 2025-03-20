using System;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Parts;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimal : IEatable, IAnimalStateChanged, IActionable, IMovable
    {
        Guid Id { get; }
        string Name { get; set; }
        int Age { get; set; }
        DateTime BirthDate { get; }
        ILivingEnvironment LivingEnvironment { get; set; }
        Eyes Eyes { get; set; }
        Legs Legs { get; set; }
        void UpdateState(IAnimalStateService stateService);
        void OnHungryStateChanged(bool isHungry, double hoursSinceFeeding);
        void OnHappinessStateChanged(bool isHappy, string environmentName);
        void OnDeathStateChanged(double hoursWithoutFood);
    }
}