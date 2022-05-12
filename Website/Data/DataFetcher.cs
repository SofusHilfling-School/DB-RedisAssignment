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
        string url = "https://datausa.io/api/data?measures=Average%20Wage,Average%20Wage%20Appx%20MOE&drilldowns=Detailed%20Occupation";
        Root<AverageWageDatum> root;
        if (useCache)
            root = await _cacheService.GetData<AverageWageDatum>(url);
        else
        {
            root = await _httpClient.GetFromJsonAsync<Root<AverageWageDatum>>(url);
            _ = _cacheService.StoreData(url, root);
        }
        return root;
    }

    public async Task<Root<TotalNoninstructionalEmployeesDatum>> GetTotalNoninstructionalEmployees(bool useCache)
    {
        string url = "https://datausa.io/api/data?University=142832&measures=Total%20Noninstructional%20Employees&drilldowns=IPEDS%20Occupation&parents=true";
        Root<TotalNoninstructionalEmployeesDatum> root;
        if (useCache)
            root = await _cacheService.GetData<TotalNoninstructionalEmployeesDatum>(url);
        else
        {
            root = await _httpClient.GetFromJsonAsync<Root<TotalNoninstructionalEmployeesDatum>>(url);
            _ = _cacheService.StoreData(url, root);
        }
        return root;
    }
}
