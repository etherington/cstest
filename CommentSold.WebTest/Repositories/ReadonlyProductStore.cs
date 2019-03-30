using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Newtonsoft.Json;

namespace CommentSold.WebTest.Repositories
{
    public class ReadonlyProductStore
    {
        private readonly IAzureCacheStorage _cacheStorage;
        private readonly IProductRepository _productRepository;

        public ReadonlyProductStore(IAzureCacheStorage cacheStorage, IProductRepository productRepository)
        {
            _cacheStorage = cacheStorage ?? throw new ArgumentNullException(nameof(cacheStorage));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<PagedList<ProductDto>> GetProductsForUserAsync(int userId, GetProductParameters getProductParameters)
        {
            PagedList<ProductDto> result;
            string key = "user:" + userId + ":pageNumber" + getProductParameters.PageNumber + ":pageSize" +
                         getProductParameters.PageSize;

            var cachedResults = await _cacheStorage.GetObjectAsync<PagedList<ProductDto>>(key);

            if (cachedResults != null)
            {
                result = cachedResults; 
            }
            else
            {
                var productsForUserFromRepo = await _productRepository.GetProductsForUserAsync(userId, getProductParameters);
                result = Mapper.Map<PagedList<ProductDto>>(productsForUserFromRepo);
                await _cacheStorage.SetStringAsync(key, JsonConvert.SerializeObject(result));
            }

            return result;
        }

        public async Task<ProductDto> GetProductForUserAsync(int userId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
