using CleanBrilliant.Domain.BoundaryInterface;
using CleanBrilliant.Domain.Entity;
using CleanBrilliant.DTO;

namespace CleanBrilliant.Domain.Control
{
    public class ProductManager
    {
        private readonly IProductGateway _gateway;

        public ProductManager(IProductGateway gateway)
        {
            _gateway = gateway;
        }

        // CRUD operations

        // + listProducts(): List<Product>
        public async Task<List<Product>> ListProducts()
        {
            var list = new List<Product>();

            await using var reader = await _gateway.GetAll();
            while (await reader.ReadAsync())
            {
                list.Add(MapReaderToProduct(reader));
            }

            return list;
        }

        // + getProduct(productId: Int): Product
        public async Task<Product> GetProduct(int productId)
        {
            await using var reader = await _gateway.GetById(productId);

            if (!await reader.ReadAsync())
                throw new KeyNotFoundException($"Product not found: {productId}");

            return MapReaderToProduct(reader);
        }

        // + searchProducts(keyword: String): List<Product>
        public async Task<List<Product>> SearchProducts(string keyword)
        {
            var list = new List<Product>();

            await using var reader = await _gateway.Search(keyword);
            while (await reader.ReadAsync())
            {
                list.Add(MapReaderToProduct(reader));
            }

            return list;
        }

        // + listProductsByCategory(categoryId: Int): List<Product>
        public async Task<List<Product>> ListProductsByCategory(int categoryId)
        {
            var list = new List<Product>();

            await using var reader = await _gateway.GetByCategory(categoryId);
            while (await reader.ReadAsync())
            {
                list.Add(MapReaderToProduct(reader));
            }

            return list;
        }


        // For external use

        // + listProductCatalog(): List<ProductDTO>
        public async Task<List<ProductDTO>> ListProductCatalog()
        {
            var list = new List<ProductDTO>();

            await using var reader = await _gateway.GetAll();
            while (await reader.ReadAsync())
            {
                list.Add(new ProductDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("product_id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Description = reader.GetString(reader.GetOrdinal("description")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
                    Volume = reader.GetDecimal(reader.GetOrdinal("volume")),
                    Weight = reader.GetInt32(reader.GetOrdinal("weight")),
                    Ingredients = reader.GetString(reader.GetOrdinal("ingredients")),
                    UnitCost = reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                    SellPrice = reader.GetDecimal(reader.GetOrdinal("sell_price"))
                    // ToxicPercentage = reader.GetFloat(reader.GetOrdinal("toxic_percentage"))
                });
            }

            return list;
        }

        // + productExists(productId: Int): Boolean
        public Task<bool> ProductExists(int productId)
        {
            return _gateway.Exists(productId);
        }

        // // + updateProductToxicPercentage(productID: Int, toxicPercentage: Float): void
        // public async Task UpdateProductToxicPercentage(int productId, float toxicPercentage)
        // {
        //     var success = await _gateway.UpdateToxicPercentage(productId, toxicPercentage);
        //     if (!success)
        //         throw new InvalidOperationException($"Failed to update toxic percentage for product {productId}");
        // }

        // Helper method to map reader to Product entity
        private Product MapReaderToProduct(Npgsql.NpgsqlDataReader reader)
        {
            return new Product(
                productId: reader.GetInt32(reader.GetOrdinal("product_id")),
                name: reader.GetString(reader.GetOrdinal("name")),
                description: reader.GetString(reader.GetOrdinal("description")),
                categoryId: reader.GetInt32(reader.GetOrdinal("category_id")),
                volume: reader.GetDecimal(reader.GetOrdinal("volume")),
                weight: reader.GetInt32(reader.GetOrdinal("weight")),
                ingredients: reader.GetString(reader.GetOrdinal("ingredients")),
                unitCost: reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                sellPrice: reader.GetDecimal(reader.GetOrdinal("sell_price"))
                // toxicPercentage: reader.GetFloat(reader.GetOrdinal("toxic_percentage"))
            );
        }
    }
}
