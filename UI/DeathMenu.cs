using System;
using System.Collections.Generic;
using System.Threading;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Services.Animals;

namespace APPZ_lab1_v6.UI
{
    public class DeathMenu
    {
        private readonly AnimalService _animalService;
        private readonly HashSet<Guid> _buriedAnimals = new();

        public DeathMenu(AnimalService animalService) => _animalService = animalService;

        public void Show(IAnimal animal)
        {
            if (animal == null || animal.IsAlive) return;
            bool exit = false;

            while (!exit)
            {
                ConsoleOutput.ClearAndShowTitle($"ТВАРИНА {animal.Name} ПОМЕРЛА");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nНа жаль, ваша тварина померла.");
                Console.ResetColor();

                bool isBuried = _buriedAnimals.Contains(animal.Id);
                if (isBuried)
                {
                    Console.WriteLine($"\nТварина {animal.Name} вже похована.");
                    ConsoleOutput.ShowMenuItem(0, "Вийти");
                    ConsoleInput.GetMenuChoice(0);
                    exit = true;
                }
                else
                {
                    ConsoleOutput.ShowMenuItem(1, "Поплакати");
                    ConsoleOutput.ShowMenuItem(2, "Закопати");
                    ConsoleOutput.ShowMenuItem(0, "Вийти");

                    int choice = ConsoleInput.GetMenuChoice(2);
                    switch (choice)
                    {
                        case 1: Cry(animal); break;
                        case 2: Bury(animal); exit = true; break;
                        case 0: exit = true; break;
                    }
                }
            }
        }

        private void Cry(IAnimal animal)
        {
            ConsoleOutput.ClearAndShowTitle("ПЛАЧ ЗА ТВАРИНОЮ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nВи плачете за своєю померлою твариною...");
            Thread.Sleep(1000);
            Console.WriteLine("...");
            Thread.Sleep(1000);
            Console.WriteLine("......");
            Thread.Sleep(1000);
            Console.WriteLine($"Прощавай, {animal.Name}... Ми будемо тебе пам'ятати.");
            Console.ResetColor();
            ConsoleOutput.WaitForKey();
        }

        private void Bury(IAnimal animal)
        {
            ConsoleOutput.ClearAndShowTitle("ПОХОВАННЯ ТВАРИНИ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nВи закопуєте свою тварину...");
            Thread.Sleep(1000);
            Console.WriteLine("Ви насипаєте могилку...");
            Thread.Sleep(1000);
            Console.WriteLine("Ви ставите табличку з іменем...");
            Thread.Sleep(1000);
            Console.WriteLine($"R.I.P. {animal.Name}");
            Console.ResetColor();

            if (animal.LivingEnvironment != null) animal.LivingEnvironment.RemoveAnimal(animal);
            _buriedAnimals.Add(animal.Id);
            ConsoleOutput.WaitForKey();
        }
    }
}