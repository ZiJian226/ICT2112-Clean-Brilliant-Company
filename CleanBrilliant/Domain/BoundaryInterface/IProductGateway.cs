using CleanBrilliant.Domain.Entity;
using Npgsql;

namespace CleanBrilliant.Domain.BoundaryInterface
{
    public interface IProductGateway
    {
        Task<NpgsqlDataReader> GetAll();
        Task<NpgsqlDataReader> GetById(int productId);
        Task<NpgsqlDataReader> GetByCategory(int categoryId);
        Task<NpgsqlDataReader> Search(string keyword);
        Task<bool> Exists(int productId);
    }
}
