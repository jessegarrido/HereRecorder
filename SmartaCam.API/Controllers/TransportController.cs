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
    }
}