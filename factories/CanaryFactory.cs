using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Services.Parts;

namespace APPZ_lab1_v6.Factories
{
    public class CanaryFactory : IAnimalFactory
    {
        private readonly IBodyPartsService _bodyPartsService;

        public CanaryFactory(IBodyPartsService bodyPartsService) => _bodyPartsService = bodyPartsService;

        public IAnimal Create(string name, int age, string featherColor)
        {
            return new Canary(name, age, featherColor, _bodyPartsService.CreateRandomEyes(), _bodyPartsService.CreateRandomLegs(2), _bodyPartsService.CreateRandomWings());
        }
    }
}