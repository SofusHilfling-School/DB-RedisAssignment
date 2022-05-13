using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;
using MessagePack;
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
        byte[] messagePack = MessagePackSerializer.Serialize(data);
        await db.StringSetAsync(key, messagePack, TimeSpan.FromSeconds(60));
    }

    public async Task<Root<T>?> GetData<T>(string key)
    {
        IDatabase db = _connection.GetDatabase();
        byte[]? data = await db.StringGetAsync(key);

        return data is null ? null : MessagePackSerializer.Deserialize<Root<T>>(data);
    }
}
