using System;
using APPZ_lab1_v6.Models.Interfaces;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalStateService : IAnimalStateService
    {
        private const double HUNGER_THRESHOLD_HOURS = 8.0;
        private const int MAX_FEEDINGS_PER_DAY = 5;
        private const double DEATH_THRESHOLD_HOURS = 24.0;
        private readonly IGameTime _gameTime;

        public IGameTime GameTime => _gameTime;

        public AnimalStateService(IGameTime gameTime) => _gameTime = gameTime;

        public bool IsHungry(IAnimal animal)
        {
            if (!animal.IsAlive) return false;
            return (_gameTime.CurrentTime - animal.LastFeedingTime).TotalHours > HUNGER_THRESHOLD_HOURS;
        }

        public bool IsHappy(IAnimal animal)
        {
            return animal.LivingEnvironment != null &&
                   (!animal.LivingEnvironment.NeedsCleaning ||
                    (animal.LivingEnvironment is ICleanable cleanable && cleanable.IsCleanEnoughForHappiness));
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

            // Якщо тварина голодна, періодично повідомляємо про це (кожну годину)
            bool isCurrentlyHungry = IsHungry(animal);
            if (isCurrentlyHungry && hoursSinceLastCheck >= 1.0)
            {
                animal.OnHungryStateChanged(true, hoursSinceFeeding);
                animal.LastGameTimeCheck = _gameTime.CurrentTime;
            }
            // Сповіщаємо про зміну стану голоду
            else if (wasHungry != isCurrentlyHungry)
            {
                animal.OnHungryStateChanged(isCurrentlyHungry, hoursSinceFeeding);
                animal.LastGameTimeCheck = _gameTime.CurrentTime;
            }
            else
            {
                animal.LastGameTimeCheck = _gameTime.CurrentTime;
            }

            if (wasHappy != IsHappy(animal))
                animal.OnHappinessStateChanged(IsHappy(animal), animal.LivingEnvironment?.Name ?? "Невідомо");
            if (wasAlive != animal.IsAlive)
                animal.OnDeathStateChanged(hoursSinceFeeding);
        }

        public bool Feed(IAnimal animal)
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