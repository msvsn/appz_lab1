using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Models;
using APPZ_lab1_v6.Services.Animals;
using APPZ_lab1_v6.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace APPZ_lab1_v6.UI
{
    public class OwnerMenu
    {
        private readonly AnimalService _animalService;
        private readonly List<Owner> _owners;

        public OwnerMenu(AnimalService animalService)
        {
            _animalService = animalService;
            _owners = new List<Owner>();
        }

        public void Show()
        {
            bool exit = false;
            while (!exit)
            {
                ConsoleOutput.ClearAndShowTitle("МЕНЮ ХАЗЯЇВ");
                ConsoleOutput.ShowMenuItem(1, "Створити нового хазяїна");
                ConsoleOutput.ShowMenuItem(2, "Показати список хазяїв");
                ConsoleOutput.ShowMenuItem(3, "Купити тварину");
                ConsoleOutput.ShowMenuItem(4, "Показати тварин хазяїна");
                ConsoleOutput.ShowMenuItem(5, "Прибрати в домі");
                ConsoleOutput.ShowMenuItem(6, "Випустити тварину на волю");
                ConsoleOutput.ShowMenuItem(0, "Назад");

                int choice = ConsoleInput.GetMenuChoice(6);
                switch (choice)
                {
                    case 1: CreateOwner(); break;
                    case 2: ShowOwners(); break;
                    case 3: BuyAnimal(); break;
                    case 4: ShowOwnerAnimals(); break;
                    case 5: CleanHome(); break;
                    case 6: ReleaseAnimal(); break;
                    case 0: exit = true; break;
                }
            }
        }

        private void CreateOwner()
        {
            ConsoleOutput.ClearAndShowTitle("СТВОРЕННЯ НОВОГО ХАЗЯЇНА");
            string name = ConsoleInput.GetName("хазяїна");
            var owner = new Owner(name);
            _owners.Add(owner);
            ConsoleOutput.ShowSuccess($"Хазяїн {owner.Name} успішно створений!");
            ConsoleOutput.WaitForKey();
        }

        private void ShowOwners()
        {
            ConsoleOutput.ClearAndShowTitle("СПИСОК ХАЗЯЇВ");
            if (_owners.Count == 0)
                ConsoleOutput.ShowMessage("Список хазяїв порожній.");
            else
            {
                ConsoleOutput.ShowMessage($"Знайдено {_owners.Count} хазяїв:");
                for (int i = 0; i < _owners.Count; i++)
                    ConsoleOutput.ShowMessage($"{i + 1}. {_owners[i].Name} (має {_owners[i].GetAnimals().Count} тварин)");
            }
            ConsoleOutput.WaitForKey();
        }

        private void BuyAnimal()
        {
            ConsoleOutput.ClearAndShowTitle("КУПІВЛЯ ТВАРИНИ");
            if (_owners.Count == 0)
            {
                ConsoleOutput.ShowError("Створіть хазяїна!");
                ConsoleOutput.WaitForKey();
                return;
            }

            var availableAnimals = _animalService.GetAllAnimals().Where(a => a.LivingEnvironment is PetShop).ToList();
            if (availableAnimals.Count == 0)
            {
                ConsoleOutput.ShowError("У зоомагазині немає тварин!");
                ConsoleOutput.WaitForKey();
                return;
            }

            var owner = SelectOwner();
            if (owner == null) return;

            ConsoleOutput.ShowMessage("\nВиберіть тварину:");
            for (int i = 0; i < availableAnimals.Count; i++)
                ConsoleOutput.ShowMessage($"{i + 1}. {availableAnimals[i].Name} ({availableAnimals[i].GetType().Name})");

            int animalIndex = ConsoleInput.GetMenuChoice(availableAnimals.Count) - 1;
            if (animalIndex < 0) return;

            var animal = availableAnimals[animalIndex];
            if (_animalService.BuyAnimal(animal, owner))
                ConsoleOutput.ShowSuccess($"{owner.Name} купив {animal.Name}!");
            else
                ConsoleOutput.ShowError("Не вдалося купити тварину.");
            ConsoleOutput.WaitForKey();
        }

        private void ShowOwnerAnimals()
        {
            ConsoleOutput.ClearAndShowTitle("ТВАРИНИ ХАЗЯЇНА");
            var owner = SelectOwner();
            if (owner == null) return;

            var animals = owner.GetAnimals();
            ConsoleOutput.ClearAndShowTitle($"ТВАРИНИ ХАЗЯЇНА {owner.Name.ToUpper()}");
            if (animals.Count == 0)
                ConsoleOutput.ShowMessage("У хазяїна немає тварин.");
            else
            {
                ConsoleOutput.ShowMessage($"Знайдено {animals.Count} тварин:");
                for (int i = 0; i < animals.Count; i++)
                    ConsoleOutput.ShowMessage($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name}) - {(animals[i].IsAlive ? "Жива" : "Мертва")}");
            }
            ConsoleOutput.WaitForKey();
        }

        private void CleanHome()
        {
            ConsoleOutput.ClearAndShowTitle("ПРИБИРАННЯ В ДОМІ");
            var owner = SelectOwner();
            if (owner == null) return;

            var environment = owner.GetHome() as ICleanable;
            
            if (environment == null)
            {
                ConsoleOutput.ShowError("Немає за ким прибрати");
                ConsoleOutput.WaitForKey();
                return;
            }
            
            if (_animalService.CleanEnvironment(environment))
                ConsoleOutput.ShowSuccess($"{owner.Name} прибрав у домі");
            else
            {
                var gameTime = _animalService.StateService.GameTime;
                var timeRemaining = 4.0 - (gameTime.CurrentTime - environment.LastCleaningTime).TotalHours;
                if (timeRemaining > 0)
                {
                    ConsoleOutput.ShowError($"Занадто рано для прибирання. Спробуйте пізніше (приблизно через {timeRemaining:F1} годин ігрового часу).");
                }
                else
                {
                    ConsoleOutput.ShowError("Не вдалося прибрати.");
                }
            }
            ConsoleOutput.WaitForKey();
        }

        private void ReleaseAnimal()
        {
            ConsoleOutput.ClearAndShowTitle("ВИПУСК ТВАРИНИ НА ВОЛЮ");
            var owner = SelectOwner();
            if (owner == null) return;

            var animals = owner.GetAnimals();
            if (animals.Count == 0)
            {
                ConsoleOutput.ShowError("У хазяїна немає тварин!");
                ConsoleOutput.WaitForKey();
                return;
            }

            ConsoleOutput.ShowMessage("\nВиберіть тварину:");
            for (int i = 0; i < animals.Count; i++)
                ConsoleOutput.ShowMessage($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name})");

            int animalIndex = ConsoleInput.GetMenuChoice(animals.Count) - 1;
            if (animalIndex < 0) return;

            var animal = animals[animalIndex];
            if (_animalService.ReleaseAnimal(animal, owner))
                ConsoleOutput.ShowSuccess($"{animal.Name} випущено на волю!");
            else
                ConsoleOutput.ShowError("Не вдалося випустити тварину!");
            ConsoleOutput.WaitForKey();
        }

        private Owner SelectOwner()
        {
            if (_owners.Count == 0)
            {
                ConsoleOutput.ShowError("Список хазяїв порожній.");
                ConsoleOutput.WaitForKey();
                return null;
            }

            ConsoleOutput.ShowMessage("Виберіть хазяїна:");
            for (int i = 0; i < _owners.Count; i++)
                ConsoleOutput.ShowMessage($"{i + 1}. {_owners[i].Name}");

            int ownerIndex = ConsoleInput.GetMenuChoice(_owners.Count) - 1;
            return ownerIndex < 0 ? null : _owners[ownerIndex];
        }
    }
}