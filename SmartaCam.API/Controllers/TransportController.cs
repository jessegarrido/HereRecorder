using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> Play()
        {
            try
            {
                _audioRepository.PlayButtonPressedAsync();
                return Ok(await _audioRepository.GetNowPlayingAsync());
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult Record()
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

        [HttpGet]
        public IActionResult Stop()
        {
            try
            {
                _audioRepository.StopButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult SkipForward()
        {
            try
            {
                //   _audioRepository.StopButtonPressedAsync();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult SkipBack()
        {
            try
            {
                //       _audioRepository.RStopButtonPressedAsync();
                return Ok(new { value = Global.MyState });
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetState()
        {
            try
            {
                var stateTask = Task.Run(() =>
                {
                    return Global.MyState;
                });
                return Ok(await stateTask);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> PlayQueue()
        {
            try
            {
                List<string> playqueue = await _audioRepository.GetPlayQueueAsync();
                return Ok(playqueue);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> NowPlaying()
            {
                try
                {  
                    return Ok(await _audioRepository.GetNowPlayingAsync());
                }
                catch (Exception)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }
    } 