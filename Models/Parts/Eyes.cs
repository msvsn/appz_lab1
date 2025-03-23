namespace APPZ_lab1_v6.Models.Parts
{
    public class Eyes
    {
        public string Color {get; set;}
        public int Count {get; set;}
        public Eyes(string color, int count = 2) {Color = color; Count = count;}
        public override string ToString() => $"Кількість: {Count}, Колір: {Color}";
    }
}