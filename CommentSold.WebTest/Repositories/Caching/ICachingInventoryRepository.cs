using System.Threading.Tasks;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories.Caching
{
    /// <summary>
    /// Provides methods for Inventory queries.
    /// </summary>
    public interface ICachingInventoryRepository
    {
        Task<PagedList<InventoryDto>> GetInventoryForUserAsync(int userId, GetInventoryParameters getInventoryParameters);
        Task<InventoryDto> GetInventoryItemForUserAsync(int userId, int inventoryId);
    }
}