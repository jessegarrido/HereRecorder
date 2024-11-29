using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Net.Http;
using System.Text.Json;
using SmartaCam.App.Services;
using Newtonsoft.Json;

namespace SmartaCam
{
    public class TransportService : ITransportService
    {
        private readonly HttpClient _httpClient;
        public TransportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> RecordButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/record");
        }
        public async Task<string> PlayButtonPress()
        {
            // return (IActionResult)await _httpClient.GetAsync("api/transport/play");
            return await System.Text.Json.JsonSerializer.DeserializeAsync<string>
                   (await _httpClient.GetStreamAsync($"api/transport/play"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> StopButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/stop");
        }
        public async Task<IActionResult> SkipForwardButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/forward");
        }
        public async Task<IActionResult> SkipBackButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/back");
        }

        public async Task<int> GetState()
        {
            //  return await _httpClient.GetAsync($"api/transport/getstate");//, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return await System.Text.Json.JsonSerializer.DeserializeAsync<int>
           (await _httpClient.GetStreamAsync($"api/transport/getstate"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            ;
        }
        public async Task<string> NowPlaying()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<string>
         (await _httpClient.GetStreamAsync($"api/transport/nowplaying"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<string>> PlayQueue()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<List<string>>
         (await _httpClient.GetStreamAsync($"api/transport/playqueue"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
