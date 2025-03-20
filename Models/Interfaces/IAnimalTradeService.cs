using APPZ_lab1_v6.Models;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalTradeService
    {
        bool BuyAnimal(IAnimal animal, Owner owner);
        bool ReleaseAnimal(IAnimal animal, Owner owner);
    }
}