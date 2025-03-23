using System;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Services.Animals;

namespace APPZ_lab1_v6.Services.Environments
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IAnimalStateService _stateService;
        private readonly IGameTime _gameTime;
        private readonly IAutoFeeder _autoFeeder;
        private const double MINIMUM_CLEANING_INTERVAL_HOURS = 4.0;
        private DateTime _lastPetShopCleaningDate;
        private List<IAnimal> _allAnimals;

        public EnvironmentService(IAnimalStateService stateService, IGameTime gameTime, IAutoFeeder autoFeeder, List<IAnimal> animalList)
        {
            _stateService = stateService;
            _gameTime = gameTime;
            _autoFeeder = autoFeeder;
            _lastPetShopCleaningDate = _gameTime.CurrentTime.Date;
            _allAnimals = animalList;
            _gameTime.DayPassed += (sender, time) => CleanPetShopsAutomatically();
        }

        public void UpdateEnvironment(ILivingEnvironment environment)
        {
            if (environment is PetShop || environment is Wilderness)
            {
                _autoFeeder.FeedAutoFedAnimals();
            }
        }

        public bool CleanEnvironment(ICleanable environment)
        {
            if (environment == null) return false;
            var timeSinceCleaning = (_gameTime.CurrentTime - environment.LastCleaningTime).TotalHours;
            if (timeSinceCleaning < MINIMUM_CLEANING_INTERVAL_HOURS)
            {
                return false;
            }
            
            environment.LastCleaningTime = _gameTime.CurrentTime;
            return true;
        }
        
        public bool IsCleanEnoughForHappiness(ICleanable environment)
        {
            if (environment == null) return true;
            return (_gameTime.CurrentTime - environment.LastCleaningTime).TotalHours <= 24.0;
        }
        
        private void CleanPetShopsAutomatically()
        {
            if (_gameTime.CurrentTime.Date != _lastPetShopCleaningDate)
            {
                foreach (var animal in _allAnimals)
                {
                    if (animal.LivingEnvironment is PetShop petShop)
                    {
                        CleanEnvironment(petShop);
                    }
                }
                _lastPetShopCleaningDate = _gameTime.CurrentTime.Date;
            }
        }
    }
}