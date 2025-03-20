namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalFactory
    {
        IAnimal Create(string name, int age, string characteristic);
    }
}