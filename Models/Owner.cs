using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.Models
{
    public class Owner
    {
        public string Name { get; }
        private readonly OwnerHome _home;

        public Owner(string name)
        {
            Name = name;
            _home = new OwnerHome(name);
        }

        public bool AddAnimal(IAnimal animal) => _home.AddAnimal(animal);
        public bool ReleaseAnimal(IAnimal animal, ILivingEnvironment wilderness) => _home.RemoveAnimal(animal) && wilderness.AddAnimal(animal);
        public List<IAnimal> GetAnimals() => _home.GetAnimals();
        public bool CleanHome() => _home.Clean();
    }
}