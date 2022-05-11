using Website.Models;

namespace Website.Data;

public class DataFetcher
{
    private readonly HttpClient _httpClient;

    public DataFetcher(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<Root<AverageWageDatum>> GetAverageWages()
    {
        return _httpClient.GetFromJsonAsync<Root<AverageWageDatum>>("https://datausa.io/api/data?measures=Average%20Wage,Average%20Wage%20Appx%20MOE&drilldowns=Detailed%20Occupation");
    }

    public Task<Root<TotalNoninstructionalEmployeesDatum>> GetTotalNoninstructionalEmployees()
    {
        return _httpClient.GetFromJsonAsync<Root<TotalNoninstructionalEmployeesDatum>>("https://datausa.io/api/data?University=142832&measures=Total%20Noninstructional%20Employees&drilldowns=IPEDS%20Occupation&parents=true");
    }
}
