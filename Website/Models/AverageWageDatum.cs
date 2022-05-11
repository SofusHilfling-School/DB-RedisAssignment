using System.Text.Json.Serialization;

namespace Website.Models;

public class AverageWageDatum
{
    [JsonPropertyName("ID Detailed Occupation")]
    public string IDDetailedOccupation { get; set; }

    [JsonPropertyName("Detailed Occupation")]
    public string DetailedOccupation { get; set; }

    [JsonPropertyName("ID Year")]
    public int IDYear { get; set; }

    [JsonPropertyName("Year")]
    public string Year { get; set; }

    [JsonPropertyName("Average Wage")]
    public double AverageWage { get; set; }

    [JsonPropertyName("Average Wage Appx MOE")]
    public double AverageWageAppxMOE { get; set; }

    [JsonPropertyName("Slug Detailed Occupation")]
    public string SlugDetailedOccupation { get; set; }
}