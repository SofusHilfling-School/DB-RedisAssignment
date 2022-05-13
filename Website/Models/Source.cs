using System.Text.Json.Serialization;
using MessagePack;

namespace Website.Models;

[MessagePackObject]
public class Source
{
    [JsonPropertyName("measures")]
    [Key(0)]
    public List<string> Measures { get; set; }

    [JsonPropertyName("annotations")]
    [Key(1)]
    public Annotations Annotations { get; set; }

    [JsonPropertyName("name")]
    [Key(2)]
    public string Name { get; set; }
}