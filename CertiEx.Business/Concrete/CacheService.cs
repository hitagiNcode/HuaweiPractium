using CertiEx.Business.Abstract;
using CertiEx.Common.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CertiEx.Business.Concrete;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;

    public CacheService(IOptions<RedisSettings> redisSettings)
    {
        var redis = ConnectionMultiplexer.Connect(redisSettings.Value.RedisConnStr);
        _cacheDb = redis.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        return !string.IsNullOrEmpty(value) ? JsonConvert.DeserializeObject<T>(value) : default;
    }

    public bool SetData<T>(string key, T data, DateTimeOffset expirationTime)
    {
        var expiry = expirationTime.DateTime.Subtract(DateTime.Now);
        return _cacheDb.StringSet(key, JsonConvert.SerializeObject(data), expiry);
    }

    public object RemoveData(string key)
    {
        var exists = _cacheDb.KeyExists(key);
        return exists && _cacheDb.KeyDelete(key);
    }
}