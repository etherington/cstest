using System;
using System.Threading.Tasks;
using AutoMapper;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Newtonsoft.Json;

namespace CommentSold.WebTest.Repositories.Caching
{
    public class ReadonlyProductStore: IProductDtoRepository
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
            string key = "user:" + userId + "|pageNumber:" + getProductParameters.PageNumber + "|pageSize:" +
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
            ProductDto product;
            string key = "user:" + userId + "|productId:" + productId;

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
    }
}
