using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    public class TransportService : ITransportService
    {
        private readonly HttpClient _httpClient;
        public TransportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> PlayRecordButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/playrecord");
        }
        public async Task<IActionResult> PlayButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/play");
        }
        public async Task<IActionResult> StopButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/stop");
        }
        public async Task<IActionResult> SkipForwardButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/skipforward");
        }
        public async Task<IActionResult> SkipBackButtonPress()
        {
            return (IActionResult)await _httpClient.GetAsync("api/transport/skipback");
        }
    }
}