using System.Threading.Tasks;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories.Caching
{
    public interface IInventoryDtoRepository
    {
        Task<PagedList<InventoryDto>> GetInventoryForUserAsync(int userId, GetInventoryParameters getInventoryParameters);
        Task<InventoryDto> GetInventoryItemForUserAsync(int userId, int inventoryId);
    }
}