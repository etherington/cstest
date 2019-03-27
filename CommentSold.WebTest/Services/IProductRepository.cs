using CommentSold.WebTest.Data;

namespace CommentSold.WebTest.Services
{
    public interface IProductRepository
    {
        PagedList<Product> GetProductsForUser(int userId, GetProductParameters getProductParameters);
        Product GetProductForUser(int userId, int productId);
    }
}