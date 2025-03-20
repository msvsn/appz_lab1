namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAnimalActionService
    {
        bool Feed(IAnimal animal);
        bool MakeWalk(IAnimal animal);
        bool MakeRun(IAnimal animal);
        bool MakeFly(IAnimal animal);
        bool MakeCrawl(IAnimal animal);
        bool MakeSing(IAnimal animal);
        void ShowAnimalActions(IAnimal animal);
    }
}