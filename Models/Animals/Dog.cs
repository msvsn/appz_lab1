using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.Models.Animals
{
    public class Dog : Animal
    {
        public string Breed { get; set; }
        public override int MealsPerDay => 3;

        public Dog(string name, int age, string breed, Eyes eyes, Legs legs)
            : base(name, age, eyes, legs)
        {
            Breed = breed;
        }

        public override bool Run() => IsAlive;
        public override bool Walk() => IsAlive;
    }
}