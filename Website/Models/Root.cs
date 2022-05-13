using System.Text.Json.Serialization;
using MessagePack;

namespace Website.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

[MessagePackObject]
public class Root<T>
{
    [JsonPropertyName("data")]
    [Key(0)]
    public List<T> Data { get; set; }

    [JsonPropertyName("source")]
    [Key(1)]
    public List<Source> Source { get; set; }
}