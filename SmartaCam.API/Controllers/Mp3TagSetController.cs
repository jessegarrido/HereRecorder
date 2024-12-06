using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.Files.ListRevisionsMode;

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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> DeleteMp3TagSet(int id)
        {
            try
            {
                await _mp3TagSetRepository.DeleteMp3TagSetByIdAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveMp3TagSet()
        {
            return Ok(await _mp3TagSetRepository.GetActiveMp3TagSetAsync());

        }
        [HttpGet]
        public async Task<IActionResult> GetAllMp3TagSets()
        {
            return Ok(await _mp3TagSetRepository.GetAllMp3TagSetsAsync());

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> SetActiveMp3TagSet(int id)
        {
            //await _mp3TagSetRepository.SetActiveMp3TagSetAsync(id);
            return Ok(await _mp3TagSetRepository.SetActiveMp3TagSetAsync(id));

        }

        [HttpPost]    
        public async Task<IActionResult> AddMp3TagSet(Mp3TagSet mp3TagSet)
            {

            try
            {
                return Ok(await _mp3TagSetRepository.AddMp3TagSetAsync(mp3TagSet));
               // var alreadyexists = await _mp3TagSetRepository.CheckIfMp3TagSetExistsAsync(mp3TagSet);
               // if (alreadyexists)
             //   {
               //    return BadRequest($"Tag set already exists");
               // } else
              //  { 
              //      await _mp3TagSetRepository.AddMp3TagSetAsync(mp3TagSet);
              //      return Ok();
             // }
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateActiveMp3TagSet(int id)
        {
            try
            {
                await _mp3TagSetRepository.SetActiveMp3TagSetAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}