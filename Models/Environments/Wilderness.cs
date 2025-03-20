using APPZ_lab1_v6.Models.Environments;

namespace APPZ_lab1_v6.Models.Environments
{
    public class Wilderness : LivingEnvironment
    {
        public override string Name => "На волі";
        public override bool NeedsCleaning => false;
    }
}