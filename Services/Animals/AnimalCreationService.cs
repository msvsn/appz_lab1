using System;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Factories;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalCreationService : IAnimalCreationService
    {
        private readonly PetShop _petShop;
        private readonly List<IAnimal> _allAnimals;
        private readonly Dictionary<Type, IAnimalFactory> _factories;

        public AnimalCreationService(PetShop petShop, IBodyPartsService bodyPartsService, List<IAnimal> allAnimals)
        {
            _petShop = petShop;
            _allAnimals = allAnimals;
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
            return animal;
        }
    }
}