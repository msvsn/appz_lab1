using System;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Services.Animals;
using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.Services
{
    public class AutoFeeder : IAutoFeeder
    {
        private readonly IAnimalStateService _stateService;
        private readonly HashSet<Guid> _autoFedAnimals = new();

        public AutoFeeder(IAnimalStateService stateService)
        {
            _stateService = stateService;
        }

        public void EnableAutoFeeding(IAnimal animal)
        {
            if (animal.IsAlive) 
            {
                _autoFedAnimals.Add(animal.Id);
                if (_stateService.NeedsFeeding(animal) || _stateService.IsHungry(animal))
                {
                    _stateService.Feed(animal);
                }
            }
        }

        public void DisableAutoFeeding(IAnimal animal) => _autoFedAnimals.Remove(animal.Id);

        public bool IsAutoFeedingEnabled(IAnimal animal) => _autoFedAnimals.Contains(animal.Id);

        public void FeedAutoFedAnimals()
        {
            foreach (var animalId in _autoFedAnimals)
            {
                var animal = AnimalService.Instance.GetAnimalById(animalId);
                if (animal != null && animal.IsAlive && _stateService.NeedsFeeding(animal))
                {
                    _stateService.Feed(animal);
                }
            }
        }

        public void EnableAutoFeedingForEnvironment(ILivingEnvironment environment)
        {
            if (environment == null) return;
            
            foreach (var animal in environment.GetAnimals())
            {
                EnableAutoFeeding(animal);
            }
        }
    }
}