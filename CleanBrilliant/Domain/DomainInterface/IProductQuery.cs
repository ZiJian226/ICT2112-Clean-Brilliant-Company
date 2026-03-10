using CleanBrilliant.DTO;

namespace CleanBrilliant.Domain.DomainInterface
{
    public interface IProductQuery
    {
        Task<bool> ProductExists(int productId);

        Task<List<ProductDTO>> ListProductCatalog();

    }
}
