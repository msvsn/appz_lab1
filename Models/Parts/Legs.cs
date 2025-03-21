namespace APPZ_lab1_v6.Models.Parts
{
    public class Legs
    {
        public double Length { get; set; }
        public int Count { get; set; }
        public Legs(double length = 25.0, int count = 4)
        {
            Length = length;
            Count = count;
        }

        public override string ToString() => $"Кількість: {Count}, Довжина: {Length} см";
    }
}