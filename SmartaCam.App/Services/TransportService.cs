using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Net.Http;
using System.Text.Json;
using SmartaCam.App.Services;
using Newtonsoft.Json;

namespace SmartaCam
{
	public interface ITransportService
	{
		//Task<Mp3TagSet> GetMp3TagSet(int id);
		//Task AddMp3TagSet(Mp3TagSet mp3TagSet);
		//Task<List<Mp3TagSet>> GetAllMp3TagSets();
		Task<IActionResult> RecordButtonPress();
		Task<IActionResult> PlayButtonPress();

		Task<IActionResult> PlayATake(int id);

		Task<IActionResult> StopButtonPress();
		Task<IActionResult> SkipForwardButtonPress();
		Task<IActionResult> SkipBackButtonPress();
		Task<int> GetState();
		Task<string> NowPlaying();
		Task<IEnumerable<string>> PlayQueue();

	}
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
            return await _httpClient.GetAsync("api/transport/play") as IActionResult;

        }

        public async Task<IActionResult> PlayATake(int id)
        {
            return await _httpClient.GetAsync($"api/transport/play/{id}") as IActionResult;

        }
            public async Task<IActionResult> StopButtonPress()
        {
            return await _httpClient.GetAsync("api/transport/stop") as IActionResult;
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
            // return await System.Text.Json.JsonSerializer.DeserializeAsync<string>
            //   (await _httpClient.GetStreamAsync($"api/transport/nowplaying"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return await _httpClient.GetStringAsync($"api/transport/nowplaying");
        }
        public async Task<IEnumerable<string>> PlayQueue()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<List<string>>
         (await _httpClient.GetStreamAsync($"api/transport/playqueue"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
