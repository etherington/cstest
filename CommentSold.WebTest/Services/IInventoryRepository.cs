using CommentSold.WebTest.Data;

namespace CommentSold.WebTest.Services
{
    public interface IInventoryRepository
    {
        PagedList<Inventory> GetInventoryForUser(int userId, GetInventoryParameters getInventoryParameters);
        Inventory GetInventoryItemForUser(int userId, int inventoryId);
    }
}