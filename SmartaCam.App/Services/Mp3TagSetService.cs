using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text;
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
        public async Task<Mp3TagSet> SetActiveMp3TagSet(int id)
        {

           // return (IActionResult)await _httpClient.GetAsync($"api/SetActiveMp3TagSet/{id}");
            return await JsonSerializer.DeserializeAsync<Mp3TagSet>
                   (await _httpClient.GetStreamAsync($"api/setactivemp3tagset/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<Mp3TagSet> GetActiveMp3TagSet()
        {
            return await JsonSerializer.DeserializeAsync<Mp3TagSet>
                (await _httpClient.GetStreamAsync($"api/getactivemp3tagset"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //throw new NotImplementedException();
        }
        public async Task<int> AddMp3TagSet(Mp3TagSet mp3TagSet)
        {
            var mp3TagSetJson = new StringContent(JsonSerializer.Serialize(mp3TagSet), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/addmp3tagset", mp3TagSetJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }
        public async Task<IActionResult> DeleteMp3TagSet(int id)
        {


            return await _httpClient.GetAsync($"api/deletemp3tagset/{id}") as IActionResult;
        }
        public async Task<List<Mp3TagSet>> GetAllMp3TagSets()
        {

            return await JsonSerializer.DeserializeAsync<List<Mp3TagSet>>
        (await _httpClient.GetStreamAsync($"api/getallmp3tagsets"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //  throw new NotImplementedException();
        }

    }
}
