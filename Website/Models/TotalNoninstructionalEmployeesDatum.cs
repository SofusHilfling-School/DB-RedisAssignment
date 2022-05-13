using System.Text.Json.Serialization;
using MessagePack;

namespace Website.Models;

[MessagePackObject]
public class TotalNoninstructionalEmployeesDatum
{
    [JsonPropertyName("ID IPEDS Occupation Parent")]
    [Key(0)]
    public string IDIPEDSOccupationParent { get; set; }

    [JsonPropertyName("IPEDS Occupation Parent")]
    [Key(1)]
    public string IPEDSOccupationParent { get; set; }

    [JsonPropertyName("ID IPEDS Occupation")]
    [Key(2)]
    public string IDIPEDSOccupation { get; set; }

    [JsonPropertyName("IPEDS Occupation")]
    [Key(3)]
    public string IPEDSOccupation { get; set; }

    [JsonPropertyName("ID Year")]
    [Key(4)]
    public int IDYear { get; set; }

    [JsonPropertyName("Year")]
    [Key(5)]
    public string Year { get; set; }

    [JsonPropertyName("ID Carnegie Parent")]
    [Key(6)]
    public string IDCarnegieParent { get; set; }

    [JsonPropertyName("Carnegie Parent")]
    [Key(7)]
    public string CarnegieParent { get; set; }

    [JsonPropertyName("ID Carnegie")]
    [Key(8)]
    public string IDCarnegie { get; set; }

    [JsonPropertyName("Carnegie")]
    [Key(9)]
    public string Carnegie { get; set; }

    [JsonPropertyName("ID University")]
    [Key(10)]
    public string IDUniversity { get; set; }

    [JsonPropertyName("University")]
    [Key(11)]
    public string University { get; set; }

    [JsonPropertyName("Total Noninstructional Employees")]
    [Key(12)]
    public int TotalNoninstructionalEmployees { get; set; }

    [JsonPropertyName("Slug University")]
    [Key(13)]
    public string SlugUniversity { get; set; }
}