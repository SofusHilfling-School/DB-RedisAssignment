using System.Text.Json.Serialization;
using MessagePack;

namespace Website.Models;

[MessagePackObject]
public class AverageWageDatum
{
    [JsonPropertyName("ID Detailed Occupation")]
    [Key(0)]
    public string IDDetailedOccupation { get; set; }

    [JsonPropertyName("Detailed Occupation")]
    [Key(1)]
    public string DetailedOccupation { get; set; }

    [JsonPropertyName("ID Year")]
    [Key(2)]
    public int IDYear { get; set; }

    [JsonPropertyName("Year")]
    [Key(3)]
    public string Year { get; set; }

    [JsonPropertyName("Average Wage")]
    [Key(4)]
    public double AverageWage { get; set; }

    [JsonPropertyName("Average Wage Appx MOE")]
    [Key(5)]
    public double AverageWageAppxMOE { get; set; }

    [JsonPropertyName("Slug Detailed Occupation")]
    [Key(6)]
    public string SlugDetailedOccupation { get; set; }
}