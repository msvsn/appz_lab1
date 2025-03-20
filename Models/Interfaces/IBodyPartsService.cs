using APPZ_lab1_v6.Models.Parts;

namespace APPZ_lab1_v6.Models.Interfaces
{
    public interface IBodyPartsService
    {
        Eyes CreateRandomEyes();
        Legs CreateRandomLegs(int count);
        Wings CreateRandomWings();
    }
}