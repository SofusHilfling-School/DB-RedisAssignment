using System.Diagnostics;
using Website.Models;

namespace Website.Data;

public class DataFetcher
{
    private readonly HttpClient _httpClient;
    private readonly RedisCacheService _cacheService;

    public DataFetcher(HttpClient httpClient, RedisCacheService cacheService)
    {
        _httpClient = httpClient;
        _cacheService = cacheService;
    }

    public async Task<Root<AverageWageDatum>> GetAverageWages(bool useCache)
    {
        Root<AverageWageDatum> root;
        if (useCache)
            root = await _cacheService.GetAverageWageData();
        else
        {
            root = await _httpClient.GetFromJsonAsync<Root<AverageWageDatum>>("https://datausa.io/api/data?measures=Average%20Wage,Average%20Wage%20Appx%20MOE&drilldowns=Detailed%20Occupation");
            _cacheService.StoreAverageWageDatum(root);
        }
        
        return root;
    }

    public async Task<Root<TotalNoninstructionalEmployeesDatum>> GetTotalNoninstructionalEmployees(bool useCache)
    {
        Root<TotalNoninstructionalEmployeesDatum> root;
        if (useCache)
            root = await _cacheService.GetTotalNoninstructionalEmployeesData();
        else
        {
            root = await _httpClient.GetFromJsonAsync<Root<TotalNoninstructionalEmployeesDatum>>("https://datausa.io/api/data?University=142832&measures=Total%20Noninstructional%20Employees&drilldowns=IPEDS%20Occupation&parents=true");
            _cacheService.StoreTotalNoninstructionalEmployeesData(root);
        }

        return root;
    }
}
