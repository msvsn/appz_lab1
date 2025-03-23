namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IEnvironmentService
    {
        void UpdateEnvironment(ILivingEnvironment environment);
        bool CleanEnvironment(ICleanable environment);
        bool IsCleanEnoughForHappiness(ICleanable environment);
    }
}