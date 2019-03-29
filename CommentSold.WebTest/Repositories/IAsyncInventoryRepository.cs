﻿using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories
{
    public interface IAsyncInventoryRepository
    {
        Task<PagedList<Inventory>> GetInventoryForUserAsync(int userId, GetInventoryParameters getInventoryParameters);
        Task<Inventory> GetInventoryItemForUserAsync(int userId, int inventoryId);
    }
}