using System;
using System.Collections.Generic;
using System.Linq;
using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalService
    {
        public static AnimalService Instance { get; private set; }
        private readonly IAnimalCreationService _creationService;
        private readonly IAnimalStateService _stateService;
        private readonly IAnimalActionService _actionService;
        private readonly IAnimalTradeService _tradeService;
        private readonly IEnvironmentService _environmentService;
        private readonly IGameTime _gameTime;
        private readonly IAutoFeeder _autoFeeder;
        private readonly List<IAnimal> _allAnimals;
        private readonly PetShop _petShop;
        private readonly Wilderness _wilderness;

        public IAnimalStateService StateService => _stateService;
        public IAnimalActionService ActionService => _actionService;

        public AnimalService(
            IAnimalCreationService creationService,
            IAnimalStateService stateService,
            IAnimalActionService actionService,
            IAnimalTradeService tradeService,
            IEnvironmentService environmentService,
            IGameTime gameTime,
            IAutoFeeder autoFeeder,
            PetShop petShop,
            Wilderness wilderness,
            List<IAnimal> animalList)
        {
            Instance = this;
            _creationService = creationService;
            _stateService = stateService;
            _actionService = actionService;
            _tradeService = tradeService;
            _environmentService = environmentService;
            _gameTime = gameTime;
            _autoFeeder = autoFeeder;
            _petShop = petShop;
            _wilderness = wilderness;
            _allAnimals = animalList;

            _gameTime.HourPassed += (sender, time) => UpdateAll();
            _gameTime.Start();
        }

        public T Create<T>(string name, int age, string characteristic) where T : IAnimal => _creationService.Create<T>(name, age, characteristic);
        public bool BuyAnimal(IAnimal animal, Owner owner) => _tradeService.BuyAnimal(animal, owner);
        public bool ReleaseAnimal(IAnimal animal, Owner owner) => _tradeService.ReleaseAnimal(animal, owner);
        public bool FeedAnimal(IAnimal animal) => _actionService.Feed(animal);
        public bool CleanEnvironment(ICleanable environment) => _environmentService.CleanEnvironment(environment);
        public List<IAnimal> GetAllAnimals() => new List<IAnimal>(_allAnimals);
        public IAnimal GetAnimalByName(string name) => _allAnimals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        public IAnimal GetAnimalById(Guid id) => _allAnimals.FirstOrDefault(a => a.Id == id);

        public void UpdateAll()
        {
            foreach (var animal in _allAnimals) _stateService.UpdateState(animal);
            _environmentService.UpdateEnvironment(_petShop);
            _environmentService.UpdateEnvironment(_wilderness);
            _autoFeeder.FeedAutoFedAnimals();
        }
    }
}