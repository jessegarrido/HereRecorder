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
    }
}