using System.Text.Json.Serialization;

namespace Website.Models;

public class Annotations
{
    [JsonPropertyName("source_name")]
    public string SourceName { get; set; }

    [JsonPropertyName("source_description")]
    public string SourceDescription { get; set; }

    [JsonPropertyName("dataset_name")]
    public string DatasetName { get; set; }

    [JsonPropertyName("dataset_link")]
    public string DatasetLink { get; set; }

    [JsonPropertyName("subtopic")]
    public string Subtopic { get; set; }

    [JsonPropertyName("table_id")]
    public string? TableId { get; set; }

    [JsonPropertyName("topic")]
    public string Topic { get; set; }

    [JsonPropertyName("hidden_measures")]
    public string? HiddenMeasures { get; set; }
}