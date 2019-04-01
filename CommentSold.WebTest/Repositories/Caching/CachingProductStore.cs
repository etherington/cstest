using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Newtonsoft.Json;

namespace CommentSold.WebTest.Repositories.Caching
{
    /// <summary>
    /// A cache-proxied readonly repository for Product queries. 
    /// </summary>
    public class CachingProductStore: ICachingProductRepository
    {
        private readonly ICacheStorage _cacheStorage;
        private readonly IProductRepository _productRepository;

        public CachingProductStore(ICacheStorage cacheStorage, IProductRepository productRepository)
        {
            _cacheStorage = cacheStorage ?? throw new ArgumentNullException(nameof(cacheStorage));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<PagedList<ProductForListDto>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters)
        {
            PagedList<ProductForListDto> result;
            string key = "user:" + userId + "|productList" + "|pageNumber:" + getProductParameters.PageNumber + "|pageSize:" +
                         getProductParameters.PageSize;

            var cachedResults = await _cacheStorage.GetObjectAsync<PagedList<ProductForListDto>>(key);

            if (cachedResults != null)
            {
                result = cachedResults; 
            }
            else
            {
                var productsForUserFromRepo = await _productRepository.GetProductsForUserAsync(userId, getProductParameters);
                result = Mapper.Map<PagedList<ProductForListDto>>(productsForUserFromRepo); 
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(result));
            }

            return result;
        }

        public async Task<ProductDto> GetProductForUserAsync(int userId, int productId)
        {
            ProductDto product;
            string key = "user:" + userId + "|product" + "|productId:" + productId;

            var cachedResult = await _cacheStorage.GetObjectAsync<ProductDto>(key);
            if (cachedResult!=null)
            {
                product = cachedResult;
            }
            else
            {
                var productForUserFromRepo = await _productRepository.GetProductForUserAsync(userId, productId);
                product = Mapper.Map<ProductDto>(productForUserFromRepo); 
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(product));
            }
            return product;
        }

        public async Task InvalidateCache(int productId, int userId)
        {
            string key = "user:" + userId + "|product" + "|productId:" + productId;
            await _cacheStorage.DeleteAsync(key);


            // Delete all cache entries for this user
            var invalidateCalls = new List<Task>();
            var keys = _cacheStorage.ListKeys();
            if (keys != null && keys.Count > 0)
            {
                foreach (var k in keys)
                {
                    var parts = k.Split('|');
                    if (parts != null && parts.Length > 0)
                    {
                        var userPart = parts[0].Split(':');
                        if (userPart != null && userPart.Length > 1)
                        {
                            if (userPart[1] == userId.ToString())
                            {
                                invalidateCalls.Add(_cacheStorage.DeleteAsync(k)); // this deletes all cache entries for this user
                            }
                        }
                    }
                }
            }
            await Task.WhenAll(invalidateCalls);
        }
    }
}
