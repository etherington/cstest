using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories
{
    public interface IProductRepository
    {
        PagedList<Product> GetProductsForUser(int userId, GetProductParameters getProductParameters);
        Product GetProductForUser(int userId, int productId);
    }
}