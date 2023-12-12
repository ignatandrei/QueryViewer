namespace ExampleBlazor.Classes;

public class WebAPIInteraction
{
    private readonly HttpClient _httpClient;

    public WebAPIInteraction([FromKeyedServices("db")] HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetTableCount(string database,string table)
    {
        return  await _httpClient.GetFromJsonAsync<int>($"api/AdvancedSearch_{database}_{table}/GetAllCount");

    }
}
