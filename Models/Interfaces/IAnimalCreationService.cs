namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalCreationService
    {
        T Create<T>(string name, int age, string characteristic) where T : IAnimal;
    }
}