namespace CleanBrilliant.DTO
{
    public class ProductDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public string Description { get; init; } = "";
        public int CategoryId { get; init; }
        public decimal Volume { get; init; }
        public int Weight { get; init; }
        public string Ingredients { get; init; } = "";
        public decimal UnitCost { get; init; }
        public decimal SellPrice { get; init; }
        // public float ToxicPercentage { get; init; }
    }
}
