using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CommentSold.WebTest.Repositories.Caching
{
    public class AzureCacheStorage: IAzureCacheStorage
    {
        private readonly string _connectionString;
        private readonly TimeSpan _expiryTimeSpanInSeconds;
        private ConnectionMultiplexer _connection;
        private IDatabase _database;
        private IServer _server;

        public AzureCacheStorage(string connectionString, uint defaultExpiryInSeconds)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _expiryTimeSpanInSeconds = TimeSpan.FromSeconds(defaultExpiryInSeconds);
            Initialize();
        }

        private void Initialize()
        {
            _connection = ConnectionMultiplexer.Connect(_connectionString);
            _database = _connection.GetDatabase();
            _server = _connection.GetServer(_connection.GetEndPoints().First());
        }

        public async Task<string> GetStringAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? "" : value.ToString();
        }

        public async Task SetStringAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value, _expiryTimeSpanInSeconds);
        }

        public async Task SetObjectAsync(string key, object value)
        {
            await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), _expiryTimeSpanInSeconds);
        }

        public async Task<T> GetObjectAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<bool> ExistAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task DeleteAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
        
        public List<string> ListKeys()
        {
            var list = new List<string>();
            foreach (var item in _server.Keys())
            {
                list.Add(item.ToString());
            }
            return list;
        }

        public Dictionary<string, string> ListKeyValues()
        {
            var list = new Dictionary<string, string>();
            var keys = ListKeys();
            foreach (var item in keys)
            {
                if (_database.KeyType(item) == RedisType.String)
                    list.Add(item.ToString(), _database.StringGet(item));
            }
            return list;
        }
    }
}





