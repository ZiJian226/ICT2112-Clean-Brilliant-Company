using System.Data;

namespace CleanBrilliant.Domain.BoundaryInterface
{
    public interface ICategoryGateway
    {
        Task<DataSet> GetAll();
        Task<DataSet> GetById(int categoryId);
    }
}
