using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartaCam.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SettingsController : ControllerBase
    {
        private ISettingsRepository _settingsRepository;

        public SettingsController(ISettingsRepository settingsRepo)
        {
            _settingsRepository = settingsRepo;

        }
        [HttpGet("getnormalize")]
        public async Task<IActionResult> GetNormalize()
        {
            return Ok(await _settingsRepository.GetNormalizeAsync());

        }
        [HttpGet("setnormalize/{willNormalize::bool}")]
        public async Task<IActionResult> SetNormalize(bool willNormalize)
        {
            await _settingsRepository.SetNormalizeAsync(willNormalize);
            return Ok();

        }
        [HttpGet("getpush")]
        public async Task<IActionResult> GetUpload()
        {
            return Ok(await _settingsRepository.GetUploadAsync());
        }
        [HttpGet("setpush/{willUpload::bool}")]
        public async Task<IActionResult> SetUpload(bool willUpload)
        {
            await _settingsRepository.SetUploadAsync(willUpload);
            return Ok();

        }
        [HttpGet("getcopy")]
        public async Task<IActionResult> GetCopy()
        {
            return Ok(await _settingsRepository.GetCopyToUsbAsync());
        }
        [HttpGet("setcopy/{willCopy::bool}")]
        public async Task<IActionResult> SetCopy(bool willCopy)
        {
            await _settingsRepository.SetCopyToUsbAsync(willCopy);
            return Ok();

        }
        [HttpGet("getnetwork")]
        public async Task<IActionResult> GetNetworkStatus()
        {
            return Ok(await _settingsRepository.GetNetworkStatus());
        }
    }
}
