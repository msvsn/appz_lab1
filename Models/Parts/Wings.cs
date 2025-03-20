namespace APPZ_lab1_v6.Models.Parts
{
    public class Wings
    {
        public double Wingspan { get; set; }
        public int Count { get; set; }
        public Wings(double wingspan = 20.0, int count = 2)
        {
            Wingspan = wingspan;
            Count = count;
        }

        public override string ToString() => $"Кількість: {Count}, Розмах: {Wingspan} см";
    }
}