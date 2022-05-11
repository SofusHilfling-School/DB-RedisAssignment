using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Website.Models;

namespace Website.Data;

public class RedisCacheService
{
    private readonly ConnectionMultiplexer _connection;

    public RedisCacheService(ConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public void StoreData<T>(Root<T> data)
    {
        IDatabase db = _connection.GetDatabase();
    }
}
