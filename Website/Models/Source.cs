using System.Text.Json.Serialization;

namespace Website.Models;

public class Source
{
    [JsonPropertyName("measures")]
    public List<string> Measures { get; set; }

    [JsonPropertyName("annotations")]
    public Annotations Annotations { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("substitutions")]
    public List<object> Substitutions { get; set; }
}