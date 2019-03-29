using System.Linq;
using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CommentSold.WebTest.Repositories
{
    public class AsyncProductRepository: IAsyncProductRepository
    {
        private readonly ApplicationDbContext _context;

        public AsyncProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<Product>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters)
        {
            var collectionBeforePaging =
                _context.Products.Include(p => p.Inventories)
                    .Where(p => p.Admin.Id == userId)
                    .OrderBy(a => a.ProductName)
                    .AsQueryable();

            return PagedList<Product>.CreateAsync(collectionBeforePaging,
                getProductParameters.PageNumber,
                getProductParameters.PageSize);
        }

        public Task<Product> GetProductForUserAsync(int userId, int productId)
        {
            var product = _context.Products.Include(p => p.Inventories)
                .FirstOrDefaultAsync(p => p.Id == productId && p.Admin.Id == userId);
            return product;
        }
    }
}
