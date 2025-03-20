using APPZ_lab1_v6.Models.Animals;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Parts;

public class DogFactory : IAnimalFactory
{
    private readonly IBodyPartsService _bodyPartsService;

    public DogFactory(IBodyPartsService bodyPartsService) => _bodyPartsService = bodyPartsService;

    public IAnimal Create(string name, int age, string breed)
    {
        return new Dog(name, age, breed, _bodyPartsService.CreateRandomEyes(), _bodyPartsService.CreateRandomLegs(4));
    }
}