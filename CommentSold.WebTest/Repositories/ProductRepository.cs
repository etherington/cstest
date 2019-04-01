using System.Linq;
using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CommentSold.WebTest.Repositories
{
    /// <summary>
    /// Repository for Product. 
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<Product>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters)
        {
            var collectionBeforePaging =
                _context.Products
                    .Include(p => p.Inventories)
                    .Where(p => p.Admin.Id == userId)
                    .OrderBy(a => a.ProductName)
                    .AsNoTracking()
                    .AsQueryable();

            return PagedList<Product>.CreateAsync(collectionBeforePaging,
                getProductParameters.PageNumber,
                getProductParameters.PageSize);
        }

        public Task<Product> GetProductForUserAsync(int userId, int productId)
        {
            var product = _context.Products
                .Include(p => p.Inventories)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId && p.Admin.Id == userId);
            return product;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }

        public async Task<bool> UpdateProductAsync(ProductForEditDto product)
        {
            var productFromRepo = await _context.Products
                .SingleOrDefaultAsync(p=>p.Id==product.Id);
            if (productFromRepo == null)
                return false;

            productFromRepo.Update(product);
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
