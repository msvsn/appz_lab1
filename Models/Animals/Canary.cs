using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.Models.Animals
{
    public class Canary : Animal
    {
        public string FeatherColor { get; set; }
        public Wings Wings { get; set; }
        public override int MealsPerDay => 2;

        public Canary(string name, int age, string featherColor, Eyes eyes, Legs legs, Wings wings)
            : base(name, age, eyes, legs)
        {
            FeatherColor = featherColor;
            Wings = wings;
        }

        public override bool Fly() => IsAlive;
        public override bool Sing() => IsAlive;
    }
}