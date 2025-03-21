using System;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.UI
{
    public static class ConsoleOutput
    {
        private static void WriteColoredLine(string message, ConsoleColor color, string prefix = "")
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{prefix}{message}");
            Console.ResetColor();
        }

        public static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }

        public static void ShowMessage(string message) => WriteColoredLine(message, ConsoleColor.White);

        public static void ShowSuccess(string message) => WriteColoredLine(message, ConsoleColor.Green, "✓ ");

        public static void ShowError(string message) => WriteColoredLine(message, ConsoleColor.Red, "✗ ");

        public static void ShowAnimalInfo(IAnimal animal, IAnimalStateService stateService, IAnimalActionService actionService)
        {
            if (animal == null)
            {
                ShowError("Тварина не знайдена");
                return;
            }

            ClearAndShowTitle($"ІНФОРМАЦІЯ ПРО ТВАРИНУ {animal.Name}");
            Console.WriteLine($"\nІм'я: {animal.Name}");
            Console.WriteLine($"Тип: {animal.GetType().Name}");
            Console.WriteLine($"Вік: {animal.Age} років");
            Console.WriteLine($"Дата народження: {animal.BirthDate.ToShortDateString()}");
            Console.WriteLine($"Статус: {(animal.IsAlive ? "Жива" : "Мертва")}");
            if (animal.IsAlive)
            {
                Console.WriteLine($"Голодна: {(stateService.IsHungry(animal) ? "Так" : "Ні")}");
                Console.WriteLine($"Щаслива: {(stateService.IsHappy(animal) ? "Так" : "Ні")}");
            }
            Console.WriteLine($"Середовище: {animal.LivingEnvironment?.Name ?? "Не визначено"}");
            Console.WriteLine($"Останнє годування: {animal.LastFeedingTime}");
            Console.WriteLine($"Очі: {animal.Eyes}");
            Console.WriteLine($"Ноги: {animal.Legs}");

            if (animal is Canary canary) Console.WriteLine($"Крила: {canary.Wings}");
            if (animal is Dog dog) Console.WriteLine($"Порода: {dog.Breed}");
            if (animal is Canary canary2) Console.WriteLine($"Колір оперення: {canary2.FeatherColor}");
            if (animal is Lizard lizard) Console.WriteLine($"Колір шкіри: {lizard.SkinColor}");

            if (animal.IsAlive)
            {
                Console.WriteLine("\n==Демонстрація дій==");
                actionService.ShowAnimalActions(animal);
            }
        }

        public static void ClearAndShowTitle(string title)
        {
            Console.Clear();
            ShowTitle(title);
        }

        public static void ShowMenuItem(int number, string description)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{number}");
            Console.ResetColor();
            Console.WriteLine($" - {description}");
        }

        public static void WaitForKey()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}