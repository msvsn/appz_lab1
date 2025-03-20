using System;
using System.Collections.Generic;
using System.Linq;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalActionService : IAnimalActionService
    {
        private readonly IAnimalStateService _stateService;
        private readonly IGameTime _gameTime;

        public AnimalActionService(IAnimalStateService stateService, IGameTime gameTime)
        {
            _stateService = stateService;
            _gameTime = gameTime;
        }

        public bool Feed(IAnimal animal) => _stateService.Feed(animal);

        private bool PerformAction(IAnimal animal, Func<IMovable, bool> action, string actionName)
        {
            if (animal == null || !animal.IsAlive) return false;
            if (_stateService.IsHungry(animal)) return false;
            if (action(animal))
            {
                Console.WriteLine($"Тварина {animal.Name} {actionName}.");
                return true;
            }
            return false;
        }

        public bool MakeWalk(IAnimal animal) => PerformAction(animal, m => m.Walk(), "пройшлася");
        public bool MakeRun(IAnimal animal) => PerformAction(animal, m => m.Run(), "побігла");
        public bool MakeFly(IAnimal animal) => PerformAction(animal, m => m.Fly(), "полетіла");
        public bool MakeCrawl(IAnimal animal) => PerformAction(animal, m => m.Crawl(), "поповзла");
        public bool MakeSing(IAnimal animal) => PerformAction(animal, m => m.Sing(), "заспівала");

        public void ShowAnimalActions(IAnimal animal)
        {
            if (animal == null || !animal.IsAlive) return;

            if (_stateService.IsHungry(animal))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (animal is Dog)
                {
                    Console.WriteLine($"Тварина {animal.Name} сидить.");
                }
                else if (animal is Canary)
                {
                    Console.WriteLine($"Тварина {animal.Name} сидить.");
                }
                else if (animal is Lizard)
                {
                    Console.WriteLine($"Тварина {animal.Name} повзає.");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (animal is Dog)
                {
                    Console.WriteLine($"Тварина {animal.Name} швидко бігає.");
                    Console.WriteLine($"Тварина {animal.Name} стрибає.");
                }
                else if (animal is Canary)
                {
                    Console.WriteLine($"Тварина {animal.Name} літає.");
                    Console.WriteLine($"Тварина {animal.Name} співає.");
                }
                else if (animal is Lizard)
                {
                    Console.WriteLine($"Тварина {animal.Name} дуже швидко бігає.");
                }
                Console.ResetColor();
            }
        }
    }
}