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
                 (await _httpClient.GetStreamAsync($"api/getmpp3tagset/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public Task<Mp3TagSet> SetDefaultMp3TagSet(int id)
        {
            throw new NotImplementedException();
        }
        public Task AddMp3TagSet(Mp3TagSet mp3TagSet)
        {

            throw new NotImplementedException();
        }
           public async Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSets()
        {

            return await JsonSerializer.DeserializeAsync<IEnumerable<Mp3TagSet>>
     (await _httpClient.GetStreamAsync($"api/getallmp3tagsets"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
          //  throw new NotImplementedException();
        }
    }
}
