using System.Text.Json.Serialization;

namespace Website.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

public class Root<T>
{
    [JsonPropertyName("data")]
    public List<T> Data { get; set; }

    [JsonPropertyName("source")]
    public List<Source> Source { get; set; }
}