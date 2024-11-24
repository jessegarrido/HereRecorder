using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    [ApiController]
    [Route("api/{action}")]
    public class TakeController : ControllerBase
    {
        private IWavTakeRepository _wavTakeRepository;
        private IMp3TakeRepository _mp3TakeRepository;
        private IMp3TagSetRepository _mp3TagSetRepository;

        // ProductValidator validator = new ProductValidator();
        public TakeController(IWavTakeRepository wavTakeRepo, IMp3TakeRepository mp3TakeRepo, IMp3TagSetRepository mp3TagSetRepo)
        {
            _wavTakeRepository = wavTakeRepo;
            _mp3TakeRepository = mp3TakeRepo;
            _mp3TagSetRepository = mp3TagSetRepo;

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