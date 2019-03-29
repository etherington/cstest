﻿using System.Threading.Tasks;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;

namespace CommentSold.WebTest.Repositories
{
    public interface IAsyncProductRepository
    {
        Task<PagedList<Product>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters);
        Task<Product> GetProductForUserAsync(int userId, int productId);
    }
}