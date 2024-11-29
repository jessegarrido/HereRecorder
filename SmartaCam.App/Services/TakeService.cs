using System.Collections;
using System.Text.Json;

namespace SmartaCam.App.Services
{
    public class TakeService : ITakeService
    {
        private readonly HttpClient _httpClient;
        public TakeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Take>> GetAllTakesAsync()
        {
            return await JsonSerializer.DeserializeAsync<List<Take>>
                 (await _httpClient.GetStreamAsync($"api/getalltakes"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
