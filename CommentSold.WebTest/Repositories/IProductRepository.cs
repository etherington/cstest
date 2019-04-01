using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories
{
    /// <summary>
    /// Repository for Product. 
    /// </summary>
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters);
        Task<Product> GetProductForUserAsync(int userId, int productId);

        Task<Product> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(ProductForEditDto product);
        Task<bool> SaveChanges();
    }
}