namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IAutoFeeder
    {
        void EnableAutoFeeding(IAnimal animal);
        void DisableAutoFeeding(IAnimal animal);
        bool IsAutoFeedingEnabled(IAnimal animal);
        void FeedAutoFedAnimals();
        void EnableAutoFeedingForEnvironment(ILivingEnvironment environment);
        void SetActionService(IAnimalActionService actionService);
    }
}