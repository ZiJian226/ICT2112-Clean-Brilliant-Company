using CleanBrilliant.DTO;

namespace CleanBrilliant.Domain.DomainInterface
{
    public interface ICategoryQuery
    {
        Task<Boolean> CategoryExists(int categoryId);

        Task<List<CategoryDTO>> ListAllCategory();
    }
}
