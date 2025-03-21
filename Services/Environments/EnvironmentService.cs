using System;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.Services.Environments
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IAnimalStateService _stateService;
        private readonly IGameTime _gameTime;
        private readonly IAutoFeeder _autoFeeder;
        private const double MINIMUM_CLEANING_INTERVAL_HOURS = 4.0;

        public EnvironmentService(IAnimalStateService stateService, IGameTime gameTime, IAutoFeeder autoFeeder)
        {
            _stateService = stateService;
            _gameTime = gameTime;
            _autoFeeder = autoFeeder;
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
            
            return environment.Clean();
        }
    }
}