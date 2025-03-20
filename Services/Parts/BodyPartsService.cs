using System;
using APPZ_lab1_v6.Models.Parts;
using APPZ_lab1_v6.Models.Interfaces;

namespace APPZ_lab1_v6.Services.Parts
{
    public class BodyPartsService : IBodyPartsService
    {
        private readonly Random _random = new();

        public Eyes CreateRandomEyes() => new Eyes(new[] { "Чорний", "Карий", "Блакитний" }[_random.Next(3)]);
        public Legs CreateRandomLegs(int count) => new Legs(_random.Next(5, 15), count);
        public Wings CreateRandomWings() => new Wings(_random.Next(15, 25));
    }
}