using System.Threading.Tasks;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories.Caching
{
    /// <summary>
    /// Provides methods for Product queries.
    /// </summary>
    public interface ICachingProductRepository
    {
        Task<PagedList<ProductForListDto>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters);
        Task<ProductDto> GetProductForUserAsync(int userId, int productId);
        Task InvalidateCache(int productEntityId, int userId);
    }
}