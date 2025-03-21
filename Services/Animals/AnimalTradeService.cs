using System;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Models;

namespace APPZ_lab1_v6.Services.Animals
{
    public class AnimalTradeService : IAnimalTradeService
    {
        private readonly PetShop _petShop;
        private readonly Wilderness _wilderness;
        private readonly IAnimalStateService _stateService;
        private readonly IAutoFeeder _autoFeeder;

        public AnimalTradeService(PetShop petShop, Wilderness wilderness, IAnimalStateService stateService, IAutoFeeder autoFeeder)
        {
            _petShop = petShop;
            _wilderness = wilderness;
            _stateService = stateService;
            _autoFeeder = autoFeeder;
        }

        public bool BuyAnimal(IAnimal animal, Owner owner)
        {
            if (!animal.IsAlive) return false;
            if (_petShop.RemoveAnimal(animal))
            {
                _autoFeeder.DisableAutoFeeding(animal);
                
                _stateService.Feed(animal);
                SubscribeToAnimalEvents(animal);
                return owner.AddAnimal(animal);
            }
            return false;
        }

        public bool ReleaseAnimal(IAnimal animal, Owner owner)
        {
            if (!animal.IsAlive) return false;
            if (owner.ReleaseAnimal(animal, _wilderness))
            {
                _autoFeeder.EnableAutoFeeding(animal);
                return true;
            }
            return false;
        }

        private void SubscribeToAnimalEvents(IAnimal animal)
        {
            animal.HungryStateChanged += (_, __) => Console.WriteLine($"Тварина {animal.Name} голодна!");
            animal.HappinessStateChanged += (_, __) => Console.WriteLine($"Щастя тварини {animal.Name} змінилося!");
            animal.DeathStateChanged += (_, __) => Console.WriteLine($"Тварина {animal.Name} померла!");
        }
    }
}