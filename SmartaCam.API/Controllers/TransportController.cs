using Microsoft.AspNetCore.Mvc;

namespace SmartaCam.Controllers
{
    [ApiController]
    [Route("api/transport/{action}")]
    public class TransportController : ControllerBase
    {
        private IAudioRepository _audioRepository;

        public TransportController(IAudioRepository audioRepository)
        {
            _audioRepository = audioRepository;
        }
        [HttpGet]//Options("playrecord")]
        public IActionResult PlayRecord()
        {
            try
            {
               _audioRepository.RecordButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public IActionResult Stop()
        {
            try
            {
                _audioRepository.PlayButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public IActionResult Play()
        {
            try
            {
                _audioRepository.PlayButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public IActionResult SkipForward()
        {
            try
            {
                _audioRepository.RecordButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public IActionResult SkipBack()
        {
            try
            {
                _audioRepository.RecordButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}