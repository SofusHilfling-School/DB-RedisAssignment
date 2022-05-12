using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Website.Models;

namespace Website.Data;

public class RedisCacheService
{
    private const string TotalNoninscructionalEmployeeKey = "TNE";
    private const string AverageWageKey = "AW";
    private readonly ConnectionMultiplexer _connection;

    public RedisCacheService(ConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public async Task StoreTotalNoninstructionalEmployeesData(Root<TotalNoninstructionalEmployeesDatum> data)
    {
        IDatabase db = _connection.GetDatabase();

        List<RedisKey> sourceHashes = new();
        List<RedisKey> datumHashes = new();
        await db.KeyDeleteAsync(GetKeysByPattern($"datum:{nameof(TotalNoninstructionalEmployeesDatum)}:*"));
        foreach(var datum in data.Data){
            var entries = new HashEntry[] {
                new HashEntry(nameof(datum.IDIPEDSOccupationParent), datum.IDIPEDSOccupationParent),
                new HashEntry(nameof(datum.IPEDSOccupationParent), datum.IPEDSOccupationParent),
                new HashEntry(nameof(datum.IDIPEDSOccupation), datum.IDIPEDSOccupation),
                new HashEntry(nameof(datum.IPEDSOccupation), datum.IPEDSOccupation),
                new HashEntry(nameof(datum.IDYear), datum.IDYear),
                new HashEntry(nameof(datum.Year), datum.Year),
                new HashEntry(nameof(datum.IDCarnegieParent), datum.IDCarnegieParent),
                new HashEntry(nameof(datum.CarnegieParent), datum.CarnegieParent),
                new HashEntry(nameof(datum.IDCarnegie), datum.IDCarnegie),
                new HashEntry(nameof(datum.IDUniversity), datum.IDUniversity),
                new HashEntry(nameof(datum.University), datum.University),
                new HashEntry(nameof(datum.TotalNoninstructionalEmployees), datum.TotalNoninstructionalEmployees),
                new HashEntry(nameof(datum.SlugUniversity), datum.SlugUniversity)
            };
            var key = new RedisKey($"datum:{nameof(TotalNoninstructionalEmployeesDatum)}:{Guid.NewGuid()}");
            await db.HashSetAsync(key, entries);
            datumHashes.Add(key);
        }
        foreach(var source in data.Source){
            var sourceKey = await StoreSource(source, TotalNoninscructionalEmployeeKey, db);
            sourceHashes.Add(sourceKey);
        }

        RedisKey sourcesKey = new($"sources:{TotalNoninscructionalEmployeeKey}");
        await db.KeyDeleteAsync(sourcesKey);
        await db.ListRightPushAsync(sourcesKey, sourceHashes.Select(x => new RedisValue(x)).ToArray());
        
        RedisKey dataKey = new($"data:{TotalNoninscructionalEmployeeKey}");
        await db.KeyDeleteAsync(dataKey);
        await db.ListRightPushAsync(dataKey, datumHashes.Select(x => new RedisValue(x)).ToArray());

        var root = new HashEntry[]
        {
            new HashEntry("sources", new RedisValue(sourcesKey)),
            new HashEntry("data", new RedisValue(dataKey)),
            new HashEntry("timestamp", new RedisValue(DateTimeOffset.Now.ToUnixTimeSeconds().ToString())),
        };
        await db.HashSetAsync(TotalNoninscructionalEmployeeKey, root);
    }

    public async Task StoreAverageWageDatum(Root<AverageWageDatum> data)
    {
        IDatabase db = _connection.GetDatabase();

        List<RedisKey> sourceHashes = new();
        List<RedisKey> datumHashes = new();
        
        await db.KeyDeleteAsync(GetKeysByPattern($"datum:{nameof(AverageWageDatum)}:*"));
        foreach (var datum in data.Data)
        {
            var entries = new HashEntry[] {
                new HashEntry(nameof(datum.IDDetailedOccupation), datum.IDDetailedOccupation),
                new HashEntry(nameof(datum.DetailedOccupation), datum.DetailedOccupation),
                new HashEntry(nameof(datum.IDYear), datum.IDYear),
                new HashEntry(nameof(datum.Year), datum.Year),
                new HashEntry(nameof(datum.AverageWage), datum.AverageWage),
                new HashEntry(nameof(datum.AverageWageAppxMOE), datum.AverageWageAppxMOE),
                new HashEntry(nameof(datum.SlugDetailedOccupation), datum.SlugDetailedOccupation)
            };
            var key = new RedisKey($"datum:{nameof(AverageWageDatum)}:{Guid.NewGuid()}");
            await db.HashSetAsync(key, entries);
            datumHashes.Add(key);
        }
        foreach (var source in data.Source)
        {
            var sourceKey = await StoreSource(source, AverageWageKey, db);
            sourceHashes.Add(sourceKey);
        }

        RedisKey sourcesKey = new($"sources:{AverageWageKey}");
        await db.KeyDeleteAsync(sourcesKey);
        await db.ListRightPushAsync(sourcesKey, sourceHashes.Select(x => new RedisValue(x)).ToArray());

        RedisKey dataKey = new($"data:{AverageWageKey}");
        await db.KeyDeleteAsync(dataKey);
        await db.ListRightPushAsync(dataKey, datumHashes.Select(x => new RedisValue(x)).ToArray());

        var root = new HashEntry[]
        {
            new HashEntry("sources", new RedisValue(sourcesKey)),
            new HashEntry("data", new RedisValue(dataKey)),
            new HashEntry("timestamp", new RedisValue(DateTimeOffset.Now.ToUnixTimeSeconds().ToString())),
        };
        await db.HashSetAsync(AverageWageKey, root);
    }

    public async Task<Root<TotalNoninstructionalEmployeesDatum>> GetTotalNoninstructionalEmployeesData()
    {
        IDatabase db = _connection.GetDatabase();

        RedisValue sourcesKeyskey = await db.HashGetAsync(TotalNoninscructionalEmployeeKey, "sources");
        RedisValue dataKeysKey = await db.HashGetAsync(TotalNoninscructionalEmployeeKey, "data");

        RedisValue[] sourceKeys = await db.ListRangeAsync((string)sourcesKeyskey);
        RedisValue[] dataKeys = await db.ListRangeAsync((string)dataKeysKey);

        List<Source> sourcesResult = new();
        foreach (RedisValue sourceKeyValue in sourceKeys)
            sourcesResult.Add(await ReadSource(sourceKeyValue, db));

        List<TotalNoninstructionalEmployeesDatum> data = new();
        foreach (RedisValue dataKeyValue in dataKeys)
        {
            string datumKey = (string)dataKeyValue;
            TotalNoninstructionalEmployeesDatum datum = new();
            Dictionary<string, RedisValue> hashes = (await db.HashGetAllAsync(datumKey)).ToDictionary(h => (string)h.Name, h => h.Value);

            datum.IDIPEDSOccupationParent = hashes[nameof(datum.IDIPEDSOccupationParent)];
            datum.IPEDSOccupationParent = hashes[nameof(datum.IPEDSOccupationParent)];
            datum.IDIPEDSOccupation = hashes[nameof(datum.IDIPEDSOccupation)];
            datum.IPEDSOccupation = hashes[nameof(datum.IPEDSOccupation)];
            datum.IDYear = (int)hashes[nameof(datum.IDYear)];
            datum.Year = hashes[nameof(datum.Year)];
            datum.IDCarnegieParent = hashes[nameof(datum.IDCarnegieParent)];
            datum.CarnegieParent = hashes[nameof(datum.CarnegieParent)];
            datum.IDCarnegie = hashes[nameof(datum.IDCarnegie)];
            datum.IDUniversity = hashes[nameof(datum.IDUniversity)];
            datum.University = hashes[nameof(datum.University)];
            datum.TotalNoninstructionalEmployees = (int) hashes[nameof(datum.TotalNoninstructionalEmployees)];
            datum.SlugUniversity = hashes[nameof(datum.SlugUniversity)];

            data.Add(datum);
        }

        return new Root<TotalNoninstructionalEmployeesDatum> { Source = sourcesResult, Data = data };
    }

    public async Task<Root<AverageWageDatum>> GetAverageWageData()
    {
        IDatabase db = _connection.GetDatabase();

        RedisValue sourcesKeysKey = await db.HashGetAsync(AverageWageKey, "sources");
        RedisValue dataKeysKey = await db.HashGetAsync(AverageWageKey, "data");

        RedisValue[] sourceKeys = await db.ListRangeAsync((string)sourcesKeysKey);
        RedisValue[] dataKeys = await db.ListRangeAsync((string)dataKeysKey);

        List<Source> sourcesResult = new();
        foreach(RedisValue sourceKeyValue in sourceKeys)
            sourcesResult.Add(await ReadSource(sourceKeyValue, db));

        List<AverageWageDatum> data = new();
        foreach(RedisValue dataKeyValue in dataKeys)
        {
            string datumKey = (string)dataKeyValue;
            AverageWageDatum datum = new();
            Dictionary<string, RedisValue> hashes = (await db.HashGetAllAsync(datumKey)).ToDictionary(h => (string)h.Name, h => h.Value);

            datum.IDDetailedOccupation = hashes[nameof(datum.IDDetailedOccupation)];
            datum.DetailedOccupation = hashes[nameof(datum.DetailedOccupation)];
            datum.IDYear =(int) hashes[nameof(datum.IDYear)];
            datum.Year = hashes[nameof(datum.Year)];
            datum.AverageWage = (double) hashes[nameof(datum.AverageWage)];
            datum.AverageWageAppxMOE = (double)hashes[nameof(datum.AverageWageAppxMOE)];
            datum.SlugDetailedOccupation = hashes[nameof(datum.SlugDetailedOccupation)];

            data.Add(datum);
        }

        return new Root<AverageWageDatum> { Source = sourcesResult, Data = data };
    }

    public async Task<bool> IsTotalNoninstructionalEmployeesExpired()
        => await IsExpired(TotalNoninscructionalEmployeeKey);

    public async Task<bool> IsAverageWageExpired()
        => await IsExpired(AverageWageKey);

    private async Task<bool> IsExpired(string key)
    {
        IDatabase db = _connection.GetDatabase();
        RedisValue value = await db.HashGetAsync(key, new RedisValue("timestamp"));
        if (!value.IsNullOrEmpty)
            return true;
        return (long)value < DateTimeOffset.Now.AddMinutes(-30).ToUnixTimeSeconds();
    }

 
    private async Task<RedisKey> StoreSource(Source source, string dataSetKey, IDatabase db){
            var annotationsKey = await StoreAnnotations(source.Annotations, dataSetKey, db);
            var measuresKey = await StoreMeasures(source.Measures, dataSetKey, db);
            
            var entries = new HashEntry[] {
                new HashEntry(nameof(source.Name), source.Name),
                new HashEntry(nameof(source.Measures), measuresKey.ToString()),
                new HashEntry(nameof(source.Annotations), annotationsKey.ToString())
            };
            RedisKey sourceKey = new($"source:{dataSetKey}");
            await db.HashSetAsync(sourceKey, entries);
            return sourceKey;
    }

    private async Task<Source> ReadSource(RedisValue sourceKey, IDatabase db)
    {
        Source sourceResult = new();
        Dictionary<string, RedisValue> hashes = (await db.HashGetAllAsync(new RedisKey((string)sourceKey))).ToDictionary(h => (string)h.Name, h => h.Value);

        sourceResult.Measures = await ReadMeasures(hashes[nameof(sourceResult.Measures)], db);
        sourceResult.Annotations = await ReadAnnotations(hashes[nameof(sourceResult.Annotations)], db);
        sourceResult.Name = hashes[nameof(sourceResult.Name)];
        return sourceResult;
    }

    private async Task<RedisKey> StoreAnnotations(Annotations annotations, string dataSetKey, IDatabase db) {
        var entries = new List<HashEntry> {
            new HashEntry(nameof(annotations.SourceName), annotations.SourceName),
            new HashEntry(nameof(annotations.SourceDescription), annotations.SourceDescription),
            new HashEntry(nameof(annotations.DatasetName), annotations.DatasetName),
            new HashEntry(nameof(annotations.DatasetLink), annotations.DatasetLink),
            new HashEntry(nameof(annotations.Subtopic), annotations.Subtopic),
            new HashEntry(nameof(annotations.Topic), annotations.Topic)
        };
        if(annotations.TableId is not null)
            entries.Add(new HashEntry(nameof(annotations.TableId), annotations.TableId));
        if(annotations.HiddenMeasures is not null)
            entries.Add(new HashEntry(nameof(annotations.HiddenMeasures), annotations.HiddenMeasures));
        
        var key = new RedisKey($"annotations:{dataSetKey}");
        await db.HashSetAsync(key, entries.ToArray());
        return key;
    }

    private async Task<Annotations> ReadAnnotations(string key, IDatabase db)
    {
        Dictionary<string, RedisValue> hashes = (await db.HashGetAllAsync(new RedisKey((string)key))).ToDictionary(h => (string)h.Name, h => h.Value);
        Annotations annotations = new();
        annotations.SourceName = hashes[nameof(annotations.SourceName)];
        annotations.SourceDescription = hashes[nameof(annotations.SourceDescription)];
        annotations.DatasetName = hashes[nameof(annotations.DatasetName)];
        annotations.DatasetLink = hashes[nameof(annotations.DatasetLink)];
        annotations.Subtopic = hashes[nameof(annotations.Subtopic)];
        annotations.Topic = hashes[nameof(annotations.Topic)];

        if (hashes.ContainsKey(nameof(annotations.TableId)))
            annotations.TableId = hashes[nameof(annotations.TableId)];
        if (hashes.ContainsKey(nameof(annotations.HiddenMeasures)))
            annotations.HiddenMeasures = hashes[nameof(annotations.HiddenMeasures)];

        return annotations;
    }

    private async Task<RedisKey> StoreMeasures(List<string> measures, string dataSetKey, IDatabase db) {
        RedisKey measuresKey = new($"measures:{dataSetKey}");
        await db.KeyDeleteAsync(measuresKey);
        await db.ListRightPushAsync(measuresKey, measures.Select(x => new RedisValue(x)).ToArray());
        return measuresKey;
    }

    private async Task<List<string>> ReadMeasures(string key, IDatabase db)
    {
        RedisValue[] measures = await db.ListRangeAsync(key);
        return measures.Select(m => (string)m).ToList();
    }

    private RedisKey[] GetKeysByPattern(string pattern)
        => _connection
            .GetServer(_connection.GetEndPoints().First())
            .Keys(pattern: pattern)
            .ToArray(); 
    
}
