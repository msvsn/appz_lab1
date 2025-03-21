namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalActionService
    {
        bool Feed(IAnimal animal);
        void ShowAnimalActions(IAnimal animal);
    }
}