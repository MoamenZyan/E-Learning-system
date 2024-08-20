using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using StackExchange.Redis;

namespace E_Learning_Platform_API.Infrastructure.Services.RedisServices
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _redisDatabase;
        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisDatabase = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> Del(string Key)
        {
            return await _redisDatabase.KeyDeleteAsync(Key);
        }

        public async Task<string?> Get(string key)
        {
            return await _redisDatabase.StringGetAsync(key);
        }

        public async Task<bool> Set(string key, string value, TimeSpan expiration)
        {
            return await _redisDatabase.StringSetAsync(key, value, expiration);
        }
    }
}
