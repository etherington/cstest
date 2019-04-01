using System;
using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Newtonsoft.Json;

namespace CommentSold.WebTest.Repositories.Caching
{
    /// <summary>
    /// A cache-proxied readonly repository for inventory queries. 
    /// </summary>
    public class CachingInventoryStore: ICachingInventoryRepository
    {
        private readonly ICacheStorage _cacheStorage;
        private readonly IInventoryRepository _inventoryRepository;

        public CachingInventoryStore(ICacheStorage cacheStorage, IInventoryRepository inventoryRepository)
        {
            _cacheStorage = cacheStorage ?? throw new ArgumentNullException(nameof(cacheStorage));
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        }

        public async Task<PagedList<InventoryDto>> GetInventoryForUserAsync(int userId, GetInventoryParameters getInventoryParameters)
        {
            PagedList<InventoryDto> result;
            string key =  "user:" + userId  + "|inventoryList" + "|pageNumber" + getInventoryParameters.PageNumber + "|pageSize:" +
                         getInventoryParameters.PageSize +
                         (!string.IsNullOrEmpty(getInventoryParameters.Sku)
                             ? "|sku:" + getInventoryParameters.Sku
                             : "") +
                         (getInventoryParameters.MaximumQuantity.HasValue
                             ? "|maximumQuantity:" + getInventoryParameters.MaximumQuantity.Value:"") +
                         (getInventoryParameters.ProductId.HasValue
                             ? "|productId:" + getInventoryParameters.ProductId.Value : "");


            var cachedResults = await _cacheStorage.GetObjectAsync<PagedList<InventoryDto>>(key);

            if (cachedResults != null)
            {
                result = cachedResults;
            }
            else
            {
                var productsForUserFromRepo = await _inventoryRepository.GetInventoryForUserAsync(userId, getInventoryParameters);
                result = Mapper.Map<PagedList<InventoryDto>>(productsForUserFromRepo);
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(result));
            }

            return result;
        }

        public async Task<InventoryDto> GetInventoryItemForUserAsync(int userId, int inventoryId)
        {
            InventoryDto inventory;
            string key = "user:" + userId + "|inventory" + "|inventoryId:" + inventoryId;

            var cachedResult = await _cacheStorage.GetObjectAsync<InventoryDto>(key);
            if (cachedResult != null)
            {
                inventory = cachedResult;
            }
            else
            {
                var inventoryForUserFromRepo = await _inventoryRepository.GetInventoryItemForUserAsync(userId, inventoryId);
                inventory = Mapper.Map<InventoryDto>(inventoryForUserFromRepo);
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(inventory));
            }
            return inventory;
        }
    }
}
