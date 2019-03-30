using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommentSold.WebTest.Repositories
{
    public class CachedProductRepositoryDecorator : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IAzureCacheStorage _cacheStorage;

        public CachedProductRepositoryDecorator(ProductRepository productRepository,
            IAzureCacheStorage cacheStorage)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _cacheStorage = cacheStorage;
        }

        public async Task<Product> GetProductForUserAsync(int userId, int productId)
        {
            Product product;
            string key = "user:" + userId + "productId:" + productId;
          
            var cachedResult = await _cacheStorage.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedResult))
            {
                product = JsonConvert.DeserializeObject<Product>(cachedResult);
            }
            else
            {
                product = await _productRepository.GetProductForUserAsync(userId, productId);
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(product));
            }
            return product;
        }


        async Task<PagedList<Product>> IProductRepository.GetProductsForUserAsync(int userId, GetProductParameters getProductParameters)
        {
            PagedList<Product> result;
            string key = "user:" + userId + ":pageNumber" + getProductParameters.PageNumber + ":pageSize" +
                         getProductParameters.PageSize;
         
            var cachedResults = await _cacheStorage.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedResults))
            {
                result = JsonConvert.DeserializeObject<PagedList<Product>>(cachedResults);
            }
            else
            {
                result = await _productRepository.GetProductsForUserAsync(userId, getProductParameters);
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(result));
            }

            return result;
        }
    }
}
