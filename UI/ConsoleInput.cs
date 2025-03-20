using System;
using System.Text.RegularExpressions;
using APPZ_lab1_v6.Helpers;

namespace APPZ_lab1_v6.UI
{
    public static class ConsoleInput
    {
        public static string GetString(string prompt, Regex pattern, string errorMessage)
        {
            string input;
            bool isValid;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
                isValid = !string.IsNullOrEmpty(input) && pattern.IsMatch(input);
                if (!isValid) ConsoleOutput.ShowError(errorMessage);
            } while (!isValid);
            return input;
        }

        public static string GetName(string entityName) => GetString(
            $"Введіть ім'я {entityName}: ",
            ValidationRegex.NamePattern,
            "Ім'я має містити тільки літери і бути не коротшим за 2 символи.");

        public static int GetAge() => int.Parse(GetString(
            "Введіть вік (1-25): ",
            ValidationRegex.AgePattern,
            "Вік має бути числом від 1 до 25."));

        public static string GetColor(string colorName) => GetString(
            $"Введіть колір {colorName}: ",
            ValidationRegex.ColorPattern,
            "Колір має містити тільки літери.");

        public static int GetMenuChoice(int maxChoice)
        {
            string choiceStr = GetString("Ваш вибір: ", ValidationRegex.MenuChoicePattern, $"Введіть число від 0 до {maxChoice}.");
            int choice = int.Parse(choiceStr);
            if (choice > maxChoice)
            {
                ConsoleOutput.ShowError($"Введіть число від 0 до {maxChoice}.");
                return GetMenuChoice(maxChoice);
            }
            return choice;
        }
    }
}