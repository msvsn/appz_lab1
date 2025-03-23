using System;
using System.Collections.Generic;
using System.Linq;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalActionService : IAnimalActionService
    {
        private readonly AnimalStateService _stateService;
        private readonly IGameTime _gameTime;

        public AnimalActionService(AnimalStateService stateService, IGameTime gameTime)
        {
            _stateService = stateService;
            _gameTime = gameTime;
        }

        public bool Feed(IAnimal animal) => _stateService.Feed(animal);

        public void ShowAnimalActions(IAnimal animal)
        {
            if (animal == null || !animal.IsAlive) return;

            bool isHungry = _stateService.IsHungry(animal);
            Console.ForegroundColor = isHungry ? ConsoleColor.Red : ConsoleColor.Green;

            if (animal is Dog dog)
            {
                if (isHungry)
                {
                    Console.WriteLine($"Тварина {animal.Name} сидить.");
                    if (dog.Walk())
                        Console.WriteLine($"Тварина {animal.Name} повільно ходить.");
                }
                else
                {
                    if (dog.Run())
                        Console.WriteLine($"Тварина {animal.Name} швидко бігає.");
                }
            }
            else if (animal is Canary canary)
            {
                if (isHungry)
                {
                    Console.WriteLine($"Тварина {animal.Name} сидить.");
                }
                else
                {
                    if (canary.Fly())
                        Console.WriteLine($"Тварина {animal.Name} літає.");
                    if (canary.Sing())
                        Console.WriteLine($"Тварина {animal.Name} співає.");
                }
            }
            else if (animal is Lizard lizard)
            {
                if (isHungry)
                {
                    Console.WriteLine($"Тварина {animal.Name} сидить.");
                }
                else
                {
                    if (lizard.Crawl())
                        Console.WriteLine($"Тварина {animal.Name} швидко повзає.");
                }
            }
            Console.ResetColor();
        }
    }
}