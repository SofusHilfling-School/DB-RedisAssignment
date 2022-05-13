using System.Text.Json.Serialization;
using MessagePack;

namespace Website.Models;

[MessagePackObject]
public class Annotations
{
    [JsonPropertyName("source_name")]
    [Key(0)]
    public string SourceName { get; set; }

    [JsonPropertyName("source_description")]
    [Key(1)]
    public string SourceDescription { get; set; }

    [JsonPropertyName("dataset_name")]
    [Key(2)]
    public string DatasetName { get; set; }

    [JsonPropertyName("dataset_link")]
    [Key(3)]
    public string DatasetLink { get; set; }

    [JsonPropertyName("subtopic")]
    [Key(4)]
    public string Subtopic { get; set; }

    [JsonPropertyName("table_id")]
    [Key(5)]
    public string? TableId { get; set; }

    [JsonPropertyName("topic")]
    [Key(6)]
    public string Topic { get; set; }

    [JsonPropertyName("hidden_measures")]
    [Key(7)]
    public string? HiddenMeasures { get; set; }
}