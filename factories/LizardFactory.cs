using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Services.Parts;

namespace APPZ_lab1_v6.Factories
{
    public class LizardFactory : IAnimalFactory
    {
        private readonly IBodyPartsService _bodyPartsService;

        public LizardFactory(IBodyPartsService bodyPartsService) => _bodyPartsService = bodyPartsService;

        public IAnimal Create(string name, int age, string skinColor)
        {
            return new Lizard(name, age, skinColor, _bodyPartsService.CreateRandomEyes(), _bodyPartsService.CreateRandomLegs(4));
        }
    }
}