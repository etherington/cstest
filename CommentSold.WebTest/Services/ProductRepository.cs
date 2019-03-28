using System.Linq;
using CommentSold.WebTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CommentSold.WebTest.Services
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedList<Product> GetProductsForUser(int userId, GetProductParameters getProductParameters)
        {
            var collectionBeforePaging =
                _context.Products.Include(p=>p.Inventories)
                    .Where(p=>p.Admin.Id == userId)
                    .OrderBy(a => a.ProductName)
                    .AsQueryable();

            return PagedList<Product>.Create(collectionBeforePaging,
                getProductParameters.PageNumber,
                getProductParameters.PageSize);
        }

        public Product GetProductForUser(int userId, int productId)
        {
            var product = _context.Products.Include(p => p.Inventories)
                .FirstOrDefault(p => p.Id == productId && p.Admin.Id == userId);
            return product;
        }
    }
}
