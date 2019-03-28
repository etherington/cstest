using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories
{
    public interface IInventoryRepository
    {
        PagedList<Inventory> GetInventoryForUser(int userId, GetInventoryParameters getInventoryParameters);
        Inventory GetInventoryItemForUser(int userId, int inventoryId);
    }
}