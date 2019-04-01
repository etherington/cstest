using System.Linq;
using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CommentSold.WebTest.Repositories
{
    /// <summary>
    /// Repository for Inventory. 
    /// </summary>
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<Inventory>> GetInventoryForUserAsync(int userId, GetInventoryParameters getInventoryParameters)
        {
            var collectionBeforePaging =
                _context.Inventory
                    .Include(x => x.Product)
                    .Where(i => i.Product.Admin.Id == userId)
                    .OrderBy(a => a.Product.Id)
                    .AsNoTracking()
                    .AsQueryable();

            if (!string.IsNullOrEmpty(getInventoryParameters.Sku))
            {
                // trim & ignore casing
                var skuForWhereClause = getInventoryParameters.Sku
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Sku.ToLowerInvariant() == skuForWhereClause);
            }
            if (getInventoryParameters.ProductId.HasValue)
            {
                // trim & ignore casing
                var productIdFilterForWhereClause = getInventoryParameters.ProductId.Value;

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Product.Id == productIdFilterForWhereClause);
            }

            if (getInventoryParameters.MaximumQuantity.HasValue)
            {
                // trim & ignore casing
                var maximumQuantityQueryForWhereClause = getInventoryParameters.MaximumQuantity.Value;

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Quantity <= maximumQuantityQueryForWhereClause);
            }

            return PagedList<Inventory>.CreateAsync(collectionBeforePaging,
                getInventoryParameters.PageNumber,
                getInventoryParameters.PageSize);
        }

        public Task<Inventory> GetInventoryItemForUserAsync(int userId, int inventoryId)
        {
            var inventoryItem = _context.Inventory.Include(i => i.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == inventoryId && i.Product.Admin.Id == userId);
            return inventoryItem;
        }
    }
}
