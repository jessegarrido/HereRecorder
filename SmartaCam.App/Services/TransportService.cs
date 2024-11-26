using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Net.Http;
using System.Text.Json;

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
        public async Task<IActionResult> PlayButtonPress()
        {
            return (IActionResult)await _httpClient.GetStreamAsync("api/transport/play");
        }
        public async Task<IActionResult> StopButtonPress()
        {
            return (IActionResult)await _httpClient.GetStreamAsync("api/transport/stop");
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
			return await JsonSerializer.DeserializeAsync<int>
	 (await _httpClient.GetStreamAsync($"api/transport/getstate"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
			//var state = (string)await _httpClient.GetStringAsync($"api/transport/getstate");//, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
           // return int.Parse(state);
        }
    }
}
