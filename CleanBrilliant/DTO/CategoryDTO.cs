namespace CleanBrilliant.DTO
{
    public class CategoryDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public string Description { get; init; } = "";
        public bool ActivityStatus { get; init; }
    }

    public class CategoryChangeDTO
    {
        public string Name { get; init; } = "";
        public string Description { get; init; } = "";
        public bool ActivityStatus { get; init; }
    }
}
