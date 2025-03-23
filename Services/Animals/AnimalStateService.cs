using System;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalStateService : IAnimalStateService
    {
        private const double HUNGER_THRESHOLD_HOURS = 8.0;
        private const int MAX_FEEDINGS_PER_DAY = 5;
        private const double DEATH_THRESHOLD_HOURS = 24.0;
        private readonly IGameTime _gameTime;
        private IEnvironmentService _environmentService;
        public IGameTime GameTime => _gameTime;
        public AnimalStateService(IGameTime gameTime)
        {
            _gameTime = gameTime;
        }
        public void SetEnvironmentService(IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
        }

        public bool IsHungry(IAnimal animal)
        {
            if (!animal.IsAlive) return false;
            return (_gameTime.CurrentTime - animal.LastFeedingTime).TotalHours > HUNGER_THRESHOLD_HOURS;
        }

        public bool IsHappy(IAnimal animal)
        {
            if (animal.LivingEnvironment == null) return false;
            if (!animal.LivingEnvironment.NeedsCleaning) return true;
            if (_environmentService != null && animal.LivingEnvironment is ICleanable cleanable)
            {
                return _environmentService.IsCleanEnoughForHappiness(cleanable);
            }
            return false;
        }

        public bool ShouldDie(IAnimal animal)
        {
            if (!animal.IsAlive || animal.LivingEnvironment?.Name.Contains("Зоомагазин") == true) return false;
            var timeSinceFeeding = (_gameTime.CurrentTime - animal.LastFeedingTime).TotalHours;
            return timeSinceFeeding > DEATH_THRESHOLD_HOURS;
        }

        public bool NeedsFeeding(IAnimal animal)
        {
            if (!animal.IsAlive) return false;
            return (_gameTime.CurrentTime - animal.LastFeedingTime).TotalHours >= 24.0 / animal.MealsPerDay;
        }

        public void UpdateState(IAnimal animal)
        {
            bool wasHungry = IsHungry(animal);
            bool wasHappy = IsHappy(animal);
            bool wasAlive = animal.IsAlive;
            var hoursSinceFeeding = (_gameTime.CurrentTime - animal.LastFeedingTime).TotalHours;
            var hoursSinceLastCheck = (_gameTime.CurrentTime - animal.LastGameTimeCheck).TotalHours;

            if (ShouldDie(animal)) animal.IsAlive = false;

            if (_gameTime.CurrentTime.Date != animal.LastFeedingCountDate)
            {
                animal.FeedingsToday = 0;
                animal.LastFeedingCountDate = _gameTime.CurrentTime.Date;
            }

            bool isCurrentlyHungry = IsHungry(animal);
            if (isCurrentlyHungry && hoursSinceLastCheck >= 1.0)
            {
                if (animal is Animal animalImpl) animalImpl.OnHungryStateChanged(true, hoursSinceFeeding);
                animal.LastGameTimeCheck = _gameTime.CurrentTime;
            }
            else if (wasHungry != isCurrentlyHungry)
            {
                if (animal is Animal animalImpl) animalImpl.OnHungryStateChanged(isCurrentlyHungry, hoursSinceFeeding);
                animal.LastGameTimeCheck = _gameTime.CurrentTime;
            }
            else
            {
                animal.LastGameTimeCheck = _gameTime.CurrentTime;
            }

            if (wasHappy != IsHappy(animal))
                if (animal is Animal animalImpl) animalImpl.OnHappinessStateChanged(IsHappy(animal), animal.LivingEnvironment?.Name ?? "Невідомо");
            
            if (wasAlive != animal.IsAlive)
                if (animal is Animal animalImpl) animalImpl.OnDeathStateChanged(hoursSinceFeeding);
        }

        internal bool Feed(IAnimal animal)
        {
            if (!animal.IsAlive) return false;
            if (animal.FeedingsToday >= MAX_FEEDINGS_PER_DAY) return false;
            var hoursSinceLastFeeding = (_gameTime.CurrentTime - animal.LastFeedingTime).TotalHours;
            if (hoursSinceLastFeeding < 24.0 / animal.MealsPerDay) return false;
            animal.LastFeedingTime = _gameTime.CurrentTime;
            animal.FeedingsToday++;
            UpdateState(animal);
            return true;
        }
    }
}