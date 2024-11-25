using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    [ApiController]
    [Route("api/{action}")]
    public class Mp3TagSetController : ControllerBase
    {
        private IMp3TagSetRepository _mp3TagSetRepository;

        public Mp3TagSetController(IMp3TagSetRepository mp3TagSetRepo)
        {
            _mp3TagSetRepository = (Mp3TagSetRepository?)mp3TagSetRepo;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMp3TagSet(int id)
        {
            return Ok(await _mp3TagSetRepository.GetMp3TagSetByIdAsync(id));

        }
        public string Get()
        {
            return "Returning from TestController Get Method";
        }
    }

}