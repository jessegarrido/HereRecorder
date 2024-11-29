using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static Dropbox.Api.Files.ListRevisionsMode;

namespace SmartaCam
{
    public class Mp3TagSetService : IMp3TagSetService
    {
        private readonly HttpClient _httpClient;
        public Mp3TagSetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Mp3TagSet> GetMp3TagSet(int id)
        {
            return await JsonSerializer.DeserializeAsync<Mp3TagSet>
                 (await _httpClient.GetStreamAsync($"api/getmp3tagset/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetActiveMp3TagSet(int id)
        {

            return (IActionResult)await _httpClient.GetAsync($"api/setactivemp3tagset/{id}");
        }
        public async Task<Mp3TagSet> GetActiveMp3TagSet()
        {
            return await JsonSerializer.DeserializeAsync<Mp3TagSet>
                (await _httpClient.GetStreamAsync($"api/getactivemp3tagset"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //throw new NotImplementedException();
        }
        public async Task<Mp3TagSet> AddMp3TagSet(Mp3TagSet mp3TagSet)
        {
            return await JsonSerializer.DeserializeAsync<Mp3TagSet>
            (await _httpClient.GetStreamAsync($"api/addmp3tagset"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> DeleteMp3TagSet(int id)
        {

            return (IActionResult)await _httpClient.GetAsync($"api/deletemp3tagset/{id}");
            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSets()
        {

            return await JsonSerializer.DeserializeAsync<IEnumerable<Mp3TagSet>>
     (await _httpClient.GetStreamAsync($"api/getallmp3tagsets"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //  throw new NotImplementedException();
        }

    }
}
