namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface IRedisService
    {
        Task<string?> Get(string key);
        Task<bool> Set(string Key, string Value, TimeSpan expiration);
        Task<bool> Del(string Key);
    }
}
