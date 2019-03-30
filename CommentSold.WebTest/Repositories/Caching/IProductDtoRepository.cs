using System.Threading.Tasks;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories.Caching
{
    public interface IProductDtoRepository
    {
        Task<PagedList<ProductDto>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters);
        Task<ProductDto> GetProductForUserAsync(int userId, int productId);
    }
}