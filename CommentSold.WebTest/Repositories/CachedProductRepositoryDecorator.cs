using System;
using CommentSold.WebTest.Data;
using CommentSold.WebTest.Dto;
using CommentSold.WebTest.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace CommentSold.WebTest.Repositories
{
    public class CachedProductRepositoryDecorator:IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache; 
        private static readonly object CacheLockObject = new object();

        public CachedProductRepositoryDecorator(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _cache = cache;
        }

        public PagedList<Product> GetProductsForUser(int userId, GetProductParameters getProductParameters)
        {
            string key = userId + "." + getProductParameters.ToKey();
            if (!_cache.TryGetValue(key, out PagedList<Product> result))
            {
                lock (CacheLockObject)
                {
                    result = _productRepository.GetProductsForUser(userId, getProductParameters);
                    _cache.Set(key, result);
                }
            }

            return result;

        }

        public Product GetProductForUser(int userId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
