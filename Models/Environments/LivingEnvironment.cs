using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Models.Parts;

namespace APPZ_lab1_v6.Models.Environments
{
    public abstract class LivingEnvironment : ILivingEnvironment
    {
        protected List<IAnimal> Animals { get; } = new List<IAnimal>();
        public abstract string Name { get; }
        public abstract bool NeedsCleaning { get; }

        public virtual bool AddAnimal(IAnimal animal)
        {
            if (animal == null || Animals.Contains(animal)) return false;
            Animals.Add(animal);
            animal.LivingEnvironment = this;
            return true;
        }

        public virtual bool RemoveAnimal(IAnimal animal)
        {
            if (animal == null || !Animals.Contains(animal)) return false;
            Animals.Remove(animal);
            if (animal.LivingEnvironment == this) animal.LivingEnvironment = null;
            return true;
        }

        public virtual List<IAnimal> GetAnimals() => [.. Animals];
    }
}