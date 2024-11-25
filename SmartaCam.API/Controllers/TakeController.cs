using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    [ApiController]
    [Route("api/{action}")]
    public class TakeController : ControllerBase
    {
        private ITakeRepository _takeRepository;

        public TakeController(ITakeRepository takeRepo)
        {
            _takeRepository = takeRepo;

        }
        [HttpGet("{id:int}")]
        //public async Task<IActionResult> GetMp3TagSet(int id)
        //{
        //    return Ok(await _mp3TagSetRepository.GetMp3TagSetByIdAsync(id));

        //}
        public string Get()
        {
            return "Returning from TestController Get Method";
        }
    }
    
}