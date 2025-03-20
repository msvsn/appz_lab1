using System;
using APPZ_lab1_v6.Services.Animals;
using APPZ_lab1_v6.Models.Interfaces;

namespace APPZ_lab1_v6.UI
{
    public class MainMenu
    {
        private readonly AnimalService _animalService;
        private readonly AnimalMenu _animalMenu;
        private readonly OwnerMenu _ownerMenu;
        private readonly IGameTime _gameTime;

        public MainMenu(AnimalService animalService, IGameTime gameTime, IAutoFeeder autoFeeder)
        {
            _animalService = animalService;
            _gameTime = gameTime;
            _animalMenu = new AnimalMenu(animalService, autoFeeder);
            _ownerMenu = new OwnerMenu(animalService);
        }

        public void Show()
        {
            bool exit = false;
            while (!exit)
            {
                ConsoleOutput.ClearAndShowTitle("ГОЛОВНЕ МЕНЮ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Внутрішній час: {_gameTime.CurrentTime}");
                Console.ResetColor();

                ConsoleOutput.ShowMenuItem(1, "Меню тварин");
                ConsoleOutput.ShowMenuItem(2, "Меню хазяїнів");
                ConsoleOutput.ShowMenuItem(3, "Оновити стан усіх тварин");
                ConsoleOutput.ShowMenuItem(0, "Вихід");

                int choice = ConsoleInput.GetMenuChoice(3);
                switch (choice)
                {
                    case 1: _animalMenu.Show(); break;
                    case 2: _ownerMenu.Show(); break;
                    case 3: UpdateAnimals(); break;
                    case 0: exit = true; break;
                }
            }

            _gameTime.Stop();
            ConsoleOutput.ClearAndShowTitle("ДЯКУЄМО ЗА ВИКОРИСТАННЯ ПРОГРАМИ");
        }

        private void UpdateAnimals()
        {
            ConsoleOutput.ClearAndShowTitle("ОНОВЛЕННЯ СТАНУ ТВАРИН");
            _animalService.UpdateAll();
            ConsoleOutput.ShowSuccess("Стан усіх тварин оновлено.");
            ConsoleOutput.WaitForKey();
        }
    }
}