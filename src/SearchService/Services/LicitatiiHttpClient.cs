using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class LicitatiiHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public LicitatiiHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task<List<Item>> GetItemsForSearchDB()
    {
        var lastUpdate = await DB.Find<Item, string>().Sort(x => x.Descending(x => x.LastUpdatedAt)).Project(x => x.LastUpdatedAt.ToString()).ExecuteFirstAsync();
        return await _httpClient.GetFromJsonAsync<List<Item>>(_configuration["LicitatiiServiceUrl"] + "/api/licitatii?date=" + lastUpdate);
    }
}