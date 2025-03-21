using System;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Factories;
using APPZ_lab1_v6.Services;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalCreationService : IAnimalCreationService
    {
        private readonly PetShop _petShop;
        private readonly List<IAnimal> _allAnimals;
        private readonly Dictionary<Type, IAnimalFactory> _factories;
        private readonly IAutoFeeder _autoFeeder;

        public AnimalCreationService(PetShop petShop, IBodyPartsService bodyPartsService, List<IAnimal> allAnimals, IAutoFeeder autoFeeder)
        {
            _petShop = petShop;
            _allAnimals = allAnimals;
            _autoFeeder = autoFeeder;
            _factories = new Dictionary<Type, IAnimalFactory>
            {
                { typeof(Dog), new DogFactory(bodyPartsService) },
                { typeof(Canary), new CanaryFactory(bodyPartsService) },
                { typeof(Lizard), new LizardFactory(bodyPartsService) }
            };
        }

        public T Create<T>(string name, int age, string characteristic) where T : IAnimal
        {
            var factory = _factories[typeof(T)];
            var animal = (T)factory.Create(name, age, characteristic);
            _allAnimals.Add(animal);
            _petShop.AddAnimal(animal);
            animal.LastFeedingTime = DateTime.Now;
            
            // Автоматично вмикаємо автогодівлю для нових тварин у зоомагазині
            _autoFeeder.EnableAutoFeeding(animal);
            
            return animal;
        }
    }
}