using APPZ_lab1_v6.Models.Environments;
using APPZ_lab1_v6.Services.Animals;
using APPZ_lab1_v6.Services.Parts;
using APPZ_lab1_v6.UI;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Services;
using APPZ_lab1_v6.Services.Environments;

namespace APPZ_lab1_v6
{
    class Program
    {
        static void Main()
        {
            var petShop = new PetShop("Зоосвіт");
            var wilderness = new Wilderness();
            var gameTime = new GameTime();
            var bodyPartsService = new BodyPartsService();
            var animalList = new List<IAnimal>();
            
            var stateService = new AnimalStateService(gameTime);
            var autoFeeder = new AutoFeeder(stateService);
            var environmentService = new EnvironmentService(stateService, gameTime, autoFeeder, animalList);
            
            ((AnimalStateService)stateService).SetEnvironmentService(environmentService);
            
            var creationService = new AnimalCreationService(petShop, bodyPartsService, animalList, autoFeeder);
            var actionService = new AnimalActionService(stateService, gameTime);
            var tradeService = new AnimalTradeService(petShop, wilderness, stateService, autoFeeder);
            autoFeeder.SetActionService(actionService);
            tradeService.SetActionService(actionService);
            var animalService = new AnimalService(creationService, stateService, actionService, tradeService, environmentService, gameTime, autoFeeder, petShop, wilderness, animalList);
            autoFeeder.EnableAutoFeedingForEnvironment(petShop);
            autoFeeder.EnableAutoFeedingForEnvironment(wilderness);
            var mainMenu = new MainMenu(animalService, gameTime, autoFeeder);
            mainMenu.Show();
        }
    }
}