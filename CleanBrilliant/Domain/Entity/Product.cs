namespace CleanBrilliant.Domain.Entity
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string Name { get; private set; } = "";
        public string Description { get; private set; } = "";
        public int CategoryId { get; private set; }
        public decimal Volume { get; private set; }
        public int Weight { get; private set; }
        public string Ingredients { get; private set; } = "";
        public decimal UnitCost { get; private set; }
        public decimal SellPrice { get; private set; }
        // public float ToxicPercentage { get; private set; }

        
        private Product() { }

        public Product(int productId, string name, string description, int categoryId, decimal volume, int weight, string ingredients, decimal unitCost, decimal sellPrice/*, float toxicPercentage*/)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Volume = volume;
            Weight = weight;
            Ingredients = ingredients;
            UnitCost = unitCost;
            SellPrice = sellPrice;
            // ToxicPercentage = toxicPercentage;
        }

        // Domain behaviours
        public void SetName(string name) => Name = name;
        public void SetDescription(string description) => Description = description;
        public void SetCategoryId(int categoryId) => CategoryId = categoryId;
        public void SetVolume(decimal volume) => Volume = volume;
        public void SetWeight(int weight) => Weight = weight;
        public void SetIngredients(string ingredients) => Ingredients = ingredients;
        public void SetUnitCost(decimal unitCost) => UnitCost = unitCost;
        public void SetSellPrice(decimal sellPrice) => SellPrice = sellPrice;
        // public void SetToxicPercentage(float toxicPercentage) => ToxicPercentage = toxicPercentage;
    }
}
