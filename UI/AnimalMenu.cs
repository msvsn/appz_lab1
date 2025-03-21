using APPZ_lab1_v6.Services.Animals;
using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Interfaces;
using System.Collections.Generic;
using System;
using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.UI
{
    public class AnimalMenu
    {
        private readonly AnimalService _animalService;
        private readonly IAutoFeeder _autoFeeder;
        private readonly DeathMenu _deathMenu;

        public AnimalMenu(AnimalService animalService, IAutoFeeder autoFeeder)
        {
            _animalService = animalService;
            _autoFeeder = autoFeeder;
            _deathMenu = new DeathMenu(animalService);
        }

        public void Show()
        {
            bool exit = false;
            while (!exit)
            {
                ConsoleOutput.ClearAndShowTitle("МЕНЮ ТВАРИН");
                ConsoleOutput.ShowMenuItem(1, "Створити нову тварину");
                ConsoleOutput.ShowMenuItem(2, "Показати всіх тварин");
                ConsoleOutput.ShowMenuItem(3, "Показати інформацію про тварину");
                ConsoleOutput.ShowMenuItem(4, "Погодувати тварину");
                ConsoleOutput.ShowMenuItem(5, "Керувати автогодівлею");
                ConsoleOutput.ShowMenuItem(0, "Назад");

                int choice = ConsoleInput.GetMenuChoice(5);
                switch (choice)
                {
                    case 1: CreateAnimal(); break;
                    case 2: ShowAllAnimals(); break;
                    case 3: ShowAnimalInfo(); break;
                    case 4: FeedAnimal(); break;
                    case 5: ManageAutoFeeding(); break;
                    case 0: exit = true; break;
                }
            }
        }

        private void CreateAnimal()
        {
            ConsoleOutput.ClearAndShowTitle("СТВОРЕННЯ НОВОЇ ТВАРИНИ");
            ConsoleOutput.ShowMenuItem(1, "Створити собаку");
            ConsoleOutput.ShowMenuItem(2, "Створити канарку");
            ConsoleOutput.ShowMenuItem(3, "Створити ящірку");
            ConsoleOutput.ShowMenuItem(0, "Назад");

            int choice = ConsoleInput.GetMenuChoice(3);
            if (choice == 0) return;

            string name = ConsoleInput.GetName("тварини");
            int age = ConsoleInput.GetAge();
            IAnimal animal = choice switch
            {
                1 => _animalService.Create<Dog>(name, age, ConsoleInput.GetColor("породи")),
                2 => _animalService.Create<Canary>(name, age, ConsoleInput.GetColor("оперення")),
                3 => _animalService.Create<Lizard>(name, age, ConsoleInput.GetColor("шкіри")),
                _ => null
            };

            if (animal != null) ConsoleOutput.ShowSuccess($"{animal.Name} успішно створено!");
            ConsoleOutput.WaitForKey();
        }

        private void ShowAllAnimals()
        {
            ConsoleOutput.ClearAndShowTitle("СПИСОК УСІХ ТВАРИН");
            var animals = _animalService.GetAllAnimals();
            if (animals.Count == 0)
                ConsoleOutput.ShowMessage("Список тварин порожній.");
            else
            {
                ConsoleOutput.ShowMessage($"Знайдено {animals.Count} тварин:");
                for (int i = 0; i < animals.Count; i++)
                    ConsoleOutput.ShowMessage($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name}) - {(animals[i].IsAlive ? "Жива" : "Мертва")}, {animals[i].LivingEnvironment?.Name ?? "Не визначено"}");
            }
            ConsoleOutput.WaitForKey();
        }

        private void ShowAnimalInfo()
        {
            ConsoleOutput.ClearAndShowTitle("ІНФОРМАЦІЯ ПРО ТВАРИНУ");
            string name = ConsoleInput.GetName("тварини");
            var animal = _animalService.GetAnimalByName(name);
            ConsoleOutput.ShowAnimalInfo(animal, _animalService.StateService, _animalService.ActionService);
            if (animal != null && !animal.IsAlive) _deathMenu.Show(animal);
            else
            {
                ConsoleOutput.WaitForKey();
            }
        }

        private void FeedAnimal()
        {
            ConsoleOutput.ClearAndShowTitle("ГОДУВАННЯ ТВАРИНИ");
            string name = ConsoleInput.GetName("тварини");
            var animal = _animalService.GetAnimalByName(name);
            if (animal == null)
            {
                ConsoleOutput.ShowError("Тварина не знайдена");
                ConsoleOutput.WaitForKey();
                return;
            }

            if (!animal.IsAlive)
            {
                ConsoleOutput.ShowError("Тварина мертва");
                _deathMenu.Show(animal);
                return;
            }

            if (animal.LivingEnvironment is PetShop || animal.LivingEnvironment is Wilderness)
            {
                ConsoleOutput.ShowMessage("Ця тварина годується автоматично");
                ConsoleOutput.WaitForKey();
                return;
            }

            if (_animalService.FeedAnimal(animal))
            {
                ConsoleOutput.ShowSuccess($"Тварина {animal.Name} погодована");
            }
            else
            {
                ConsoleOutput.ShowError($"Не вдалося погодувати тварину {animal.Name}.");
            }
            ConsoleOutput.WaitForKey();
        }

        private void ManageAutoFeeding()
        {
            ConsoleOutput.ClearAndShowTitle("КЕРУВАННЯ АВТОГОДІВЛЕЮ");
            string name = ConsoleInput.GetName("тварини");
            var animal = _animalService.GetAnimalByName(name);
            if (animal == null)
            {
                ConsoleOutput.ShowError("Тварина не знайдена");
                ConsoleOutput.WaitForKey();
                return;
            }
            if (!animal.IsAlive)
            {
                ConsoleOutput.ShowError("Тварина мертва");
                _deathMenu.Show(animal);
                return;
            }

            bool isEnabled = _autoFeeder.IsAutoFeedingEnabled(animal);
            bool isInSpecialEnvironment = animal.LivingEnvironment is PetShop || animal.LivingEnvironment is Wilderness;
            
            Console.WriteLine($"\nАвтогодівля: {(isEnabled ? "УВІМКНЕНА" : "ВИМКНЕНА")}");
            if (isInSpecialEnvironment)
            {
                Console.WriteLine($"Тварина знаходиться в середовищі: {animal.LivingEnvironment.Name}");
            }
            
            ConsoleOutput.ShowMenuItem(1, isEnabled ? "Вимкнути автогодівлю" : "Увімкнути автогодівлю");
            ConsoleOutput.ShowMenuItem(0, "Назад");

            int choice = ConsoleInput.GetMenuChoice(1);
            if (choice == 1)
            {
                if (isEnabled)
                {
                    _autoFeeder.DisableAutoFeeding(animal);
                    ConsoleOutput.ShowSuccess($"Автогодівля для {animal.Name} вимкнена");
                }
                else
                {
                    _autoFeeder.EnableAutoFeeding(animal);
                    ConsoleOutput.ShowSuccess($"Автогодівля для {animal.Name} увімкнена");
                }
            }
            ConsoleOutput.WaitForKey();
        }
    }
}