﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentSold.WebTest.Repositories.Caching
{
    /// <summary>
    /// Provides methods for interacting with a cache.
    /// </summary>
    public interface ICacheStorage
    {
        Task SetStringAsync(string key, string value);
        Task SetObjectAsync(string key, object value);
        Task<string> GetStringAsync(string key);
        Task<T> GetObjectAsync<T>(string key);
        Task<bool> ExistAsync(string key);
        Task DeleteAsync(string key);
        List<string> ListKeys();
        Dictionary<string, string> ListKeyValues();
    }
}