using System.Linq;
using CommentSold.WebTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CommentSold.WebTest.Services
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedList<Inventory> GetInventoryForUser(int userId, GetInventoryParameters getInventoryParameters)
        {
            var collectionBeforePaging =
                _context.Inventory
                    .Include(x=>x.Product)
                    .Where(i => i.Product.Admin.Id == userId)
                    .OrderBy(a => a.Product.Id)
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

            return  PagedList<Inventory>.Create(collectionBeforePaging,
                getInventoryParameters.PageNumber,
                getInventoryParameters.PageSize);
        }

        public Inventory GetInventoryItemForUser(int userId, int inventoryId)
        {
            var inventoryItem = _context.Inventory.Include(i=>i.Product)
                .FirstOrDefault(i => i.Id == inventoryId && i.Product.Admin.Id == userId);
            return inventoryItem;
        }
    }
}

