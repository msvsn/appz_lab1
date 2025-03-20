using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Models.Animals;

namespace APPZ_lab1_v6.Models.Animals
{
    public class Lizard : Animal
    {
        public string SkinColor { get; set; }
        public override int MealsPerDay => 4;

        public Lizard(string name, int age, string skinColor, Eyes eyes, Legs legs)
            : base(name, age, eyes, legs)
        {
            SkinColor = skinColor;
        }

        public override bool Crawl() => IsAlive;
    }
}