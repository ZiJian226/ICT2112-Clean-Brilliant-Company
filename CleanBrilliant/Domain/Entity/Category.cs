namespace CleanBrilliant.Domain.Entity
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; } = "";
        public string Description { get; private set; } = "";
        public bool ActivityStatus { get; private set; }

        protected int getCategoryId() => CategoryId;


        private Category() { }

        public Category(int categoryId, string name, string description, bool activityStatus)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            ActivityStatus = activityStatus;
        }

        // Domain behaviours (optional)
        public void Rename(string name) => Name = name;
        public void SetDescription(string description) => Description = description;
        public void SetActivityStatus(bool status) => ActivityStatus = status;
    }
}
