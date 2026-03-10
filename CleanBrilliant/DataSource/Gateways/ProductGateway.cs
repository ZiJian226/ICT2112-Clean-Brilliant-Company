using CleanBrilliant.Domain.BoundaryInterface;
using CleanBrilliant.Domain.Entity;
using Npgsql;
using System.Data;

namespace CleanBrilliant.Data.Gateways
{
    public class ProductGateway : IProductGateway
    {
        private readonly string _connString;

        public ProductGateway(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection in appsettings.json");
        }

        public async Task<NpgsqlDataReader> GetAll()
        {
            const string sql = @"
                SELECT product_id, name, description, category_id, volume, weight, 
                       ingredients, unit_cost, sell_price /* , toxic_percentage */
                FROM product
                ORDER BY product_id;";

            var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(sql, conn);
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public async Task<NpgsqlDataReader> GetById(int productId)
        {
            const string sql = @"
                SELECT product_id, name, description, category_id, volume, weight, 
                       ingredients, unit_cost, sell_price /* , toxic_percentage */
                FROM product
                WHERE product_id = @id;";

            var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", productId);

            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public async Task<NpgsqlDataReader> GetByCategory(int categoryId)
        {
            const string sql = @"
                SELECT product_id, name, description, category_id, volume, weight, 
                       ingredients, unit_cost, sell_price /* , toxic_percentage */
                FROM product
                WHERE category_id = @categoryId
                ORDER BY product_id;";

            var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("categoryId", categoryId);

            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public async Task<NpgsqlDataReader> Search(string keyword)
        {
            const string sql = @"
                SELECT product_id, name, description, category_id, volume, weight, 
                       ingredients, unit_cost, sell_price /* , toxic_percentage */
                FROM product
                WHERE name ILIKE @keyword 
                   OR description ILIKE @keyword
                   OR ingredients ILIKE @keyword
                ORDER BY product_id;";

            var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("keyword", $"%{keyword}%");

            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }


        public async Task<bool> Exists(int productId)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM product
                WHERE product_id = @id;";

            await using var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", productId);

            var result = await cmd.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}
