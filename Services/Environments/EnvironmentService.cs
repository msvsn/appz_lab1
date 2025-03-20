using System;
using System.Collections.Generic;
using System.Linq;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.Services.Environments
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IAnimalStateService _stateService;
        private readonly IGameTime _gameTime;
        private DateTime _lastAutoFeedingTime;
        private const double MINIMUM_CLEANING_INTERVAL_HOURS = 4.0; // Мінімальний інтервал між прибираннями

        public EnvironmentService(IAnimalStateService stateService, IGameTime gameTime)
        {
            _stateService = stateService;
            _gameTime = gameTime;
            _lastAutoFeedingTime = _gameTime.CurrentTime;
        }

        public void UpdateEnvironment(ILivingEnvironment environment)
        {
            if (environment is PetShop petShop)
            {
                if ((_gameTime.CurrentTime - _lastAutoFeedingTime).TotalHours >= 1.0)
                {
                    foreach (var animal in petShop.GetAnimals().Where(a => a.IsAlive))
                    {
                        _stateService.Feed(animal);
                    }
                    _lastAutoFeedingTime = _gameTime.CurrentTime;
                }

                foreach (var animal in petShop.GetAnimals().Where(a => a.IsAlive && _stateService.NeedsFeeding(a)))
                {
                    _stateService.Feed(animal);
                }
            }
            else if (environment is Wilderness wilderness)
            {
                if ((_gameTime.CurrentTime - _lastAutoFeedingTime).TotalHours >= 1.0)
                {
                    foreach (var animal in wilderness.GetAnimals().Where(a => a.IsAlive))
                    {
                        _stateService.Feed(animal);
                    }
                }

                foreach (var animal in wilderness.GetAnimals().Where(a => a.IsAlive && _stateService.NeedsFeeding(a)))
                {
                    _stateService.Feed(animal);
                }
            }
        }

        public bool CleanEnvironment(ICleanable environment)
        {
            if (environment == null) return false;
            
            // Перевіряємо, чи минуло достатньо часу з останнього прибирання
            var timeSinceCleaning = (_gameTime.CurrentTime - environment.LastCleaningTime).TotalHours;
            if (timeSinceCleaning < MINIMUM_CLEANING_INTERVAL_HOURS)
            {
                return false; // Занадто рано для прибирання
            }
            
            return environment.Clean();
        }
    }
}