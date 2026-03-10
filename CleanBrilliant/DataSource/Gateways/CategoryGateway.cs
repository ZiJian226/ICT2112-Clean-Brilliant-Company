using CleanBrilliant.Domain.BoundaryInterface;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace CleanBrilliant.Data.Gateways
{
    public class CategoryGateway : ICategoryGateway
    {
        private readonly string _connString;
        private readonly string _dsName = "category";

        public CategoryGateway(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection in appsettings.json");
        }


        public async Task<DataSet> GetAll()
        {
            const string sql = @"
                SELECT category_id, name, description, activity_status
                FROM category
                ORDER BY category_id;";
            
            var ds = new DataSet();

            var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(sql, conn);
            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(ds,_dsName);
            return ds;
        }

        public async Task<DataSet> GetById(int categoryId)
        {
            const string sql = @"
                SELECT category_id, name, description, activity_status
                FROM category
                WHERE category_id = @id;";

            var ds = new DataSet();

            var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", categoryId);

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(ds, _dsName);
            return ds;
        }


    }
}
