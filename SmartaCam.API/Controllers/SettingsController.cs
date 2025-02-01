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
        [HttpGet("getnormalizesplitchannels")]
        public async Task<IActionResult> GetNormalizeSplitChannels()
        {
            return Ok(await _settingsRepository.GetNormalizeSplitChannelsAsync());

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
		[HttpGet("getdropboxstatus")]
		public async Task<IActionResult> GetDropBoxAuthStatus()
		{
			return Ok(await _settingsRepository.GetDropBoxAuthStatusAsync());
		}
		[HttpGet("getdropboxcode")]
		public async Task<IActionResult> GetDropBoxCode()
		{
			return Ok(await _settingsRepository.GetDropBoxCode());
		}
        [HttpGet("removablepath")]
        public async Task<IActionResult> GetRemovableDrivePath()
        {
            return Ok(await _settingsRepository.GetRemovableDrivePathAsync());
        }
		[HttpGet("setremovablepath/{removableDrivePath}")]
		public async Task<IActionResult> SetRemovableDrivePath(string removableDrivePath)
		{
			await _settingsRepository.SetRemovableDrivePathAsync(removableDrivePath);
            return Ok();
		}
		[HttpGet("removablepaths")]
		public async Task<IActionResult> GetRemovableDrivePaths()
		{
			return Ok(await _settingsRepository.GetRemovableDrivePathsAsync());
		}
		[HttpGet("setdropboxcode/{dropBoxCode}")]
		public async Task<IActionResult> SetDropBoxCode(string dropBoxCode)
		{
			await _settingsRepository.SetDropBoxCode(dropBoxCode);
			return Ok();
		}
		[HttpGet("unauthdropbox")]
		public async Task<IActionResult> UnAuthDropBox()
		{
			await _settingsRepository.UnAuthDropBoxAsync();
			return Ok();

		}
	}
}
