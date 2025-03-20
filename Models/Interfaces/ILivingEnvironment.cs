using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface ILivingEnvironment
    {
        string Name { get; }
        bool NeedsCleaning { get; }
        bool AddAnimal(IAnimal animal);
        bool RemoveAnimal(IAnimal animal);
        List<IAnimal> GetAnimals();
    }
}