using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;

namespace SmartaCam.App.Services
{
    public interface ITakeService
    {

        public Task<List<Take>> GetAllTakesAsync();
        public Task<TimeSpan> GetDurationById(int id);
        public Task<IActionResult> DeleteTakeById(int id);
		public Task<IActionResult> DeleteAllTakes();

	}
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
        public async Task<TimeSpan> GetDurationById(int id)
        {
            return await JsonSerializer.DeserializeAsync<TimeSpan>
                 (await _httpClient.GetStreamAsync($"api/gettakeduration/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> DeleteTakeById(int id)
        {


            return await _httpClient.GetAsync($"api/deletetake/{id}") as IActionResult;
        }
		public async Task<IActionResult> DeleteAllTakes()
		{


			return await _httpClient.GetAsync($"api/deleteall") as IActionResult;
		}
		public async Task<List<Mp3TagSet>> GetAllMp3TagSets()
        {

            return await JsonSerializer.DeserializeAsync<List<Mp3TagSet>>
        (await _httpClient.GetStreamAsync($"api/getallmp3tagsets"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //  throw new NotImplementedException();
        }
    }
}
