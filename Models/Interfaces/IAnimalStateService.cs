namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalStateService
    {
        IGameTime GameTime { get; }
        bool IsHungry(IAnimal animal);
        bool IsHappy(IAnimal animal);
        bool ShouldDie(IAnimal animal);
        bool NeedsFeeding(IAnimal animal);
        void UpdateState(IAnimal animal);
        bool Feed(IAnimal animal);
    }
}