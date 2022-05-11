using System.Text.Json.Serialization;

namespace Website.Models;

public class TotalNoninstructionalEmployeesDatum
{
    [JsonPropertyName("ID IPEDS Occupation Parent")]
    public string IDIPEDSOccupationParent { get; set; }

    [JsonPropertyName("IPEDS Occupation Parent")]
    public string IPEDSOccupationParent { get; set; }

    [JsonPropertyName("ID IPEDS Occupation")]
    public string IDIPEDSOccupation { get; set; }

    [JsonPropertyName("IPEDS Occupation")]
    public string IPEDSOccupation { get; set; }

    [JsonPropertyName("ID Year")]
    public int IDYear { get; set; }

    [JsonPropertyName("Year")]
    public string Year { get; set; }

    [JsonPropertyName("ID Carnegie Parent")]
    public string IDCarnegieParent { get; set; }

    [JsonPropertyName("Carnegie Parent")]
    public string CarnegieParent { get; set; }

    [JsonPropertyName("ID Carnegie")]
    public string IDCarnegie { get; set; }

    [JsonPropertyName("Carnegie")]
    public string Carnegie { get; set; }

    [JsonPropertyName("ID University")]
    public string IDUniversity { get; set; }

    [JsonPropertyName("University")]
    public string University { get; set; }

    [JsonPropertyName("Total Noninstructional Employees")]
    public int TotalNoninstructionalEmployees { get; set; }

    [JsonPropertyName("Slug University")]
    public string SlugUniversity { get; set; }
}