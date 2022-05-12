using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;
using Website.Models;

namespace Website.Data;

public class RedisCacheService
{
    private readonly ConnectionMultiplexer _connection;

    public RedisCacheService(ConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public async Task StoreData<T>(string key, Root<T> data)
    {
        IDatabase db = _connection.GetDatabase();
        string json = JsonSerializer.Serialize(data);
        await db.StringSetAsync(key, json);
    }

    public async Task<Root<T>> GetData<T>(string key)
    {
        IDatabase db = _connection.GetDatabase();
        string json = await db.StringGetAsync(key);
        return JsonSerializer.Deserialize<Root<T>>(json);
    }
}
