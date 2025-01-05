using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    [ApiController]
    [Route("api/")]
    public class TakeController : ControllerBase
    {
        private ITakeRepository _takeRepository;

		public TakeController(ITakeRepository takeRepo)
        {
            _takeRepository = takeRepo;

        }
        [HttpGet("gettake/{id:int}")]
        public async Task<ActionResult<Take>> GetTakeById(int id)
        {
            return Ok(await _takeRepository.GetTakeByIdAsync(id));

        }
        [HttpGet("gettakeduration/{id:int}")]
        public async Task<ActionResult<TimeSpan>> GetDurationById(int id)
        {
            return Ok(await _takeRepository.GetTakeDurationByIdAsync(id));

        }
        [HttpGet("getalltakes")]
        public async Task<ActionResult<List<Take>>> GetAllTakes()
        {
            return Ok(await _takeRepository.GetAllTakesAsync());

        }
        [HttpGet("deletetake/{id:int}")]
        public async Task<IActionResult> DeleteTake(int id)
        {
            try
            {
                await _takeRepository.DeleteTakeByIdAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
		[HttpGet("deleteall")]
		public async Task<IActionResult> DeleteAllTakes()
		{
			try
			{
                await _takeRepository.DeleteAllTakesAsync();
				return Ok();
			}
			catch (Exception)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
		//[HttpGet]
		//public async Task<ActionResult<DateTime>> GetLatestTakeDate()
		//{
		//	return Ok(await _takeRepository.GetLastTakeDateAsync());
		//}

		[HttpPost]
        public async Task<ActionResult<Take>> AddTake(Take newTake)
        {
            // return Ok(await _takeRepository.AddTakeAsync(newTake));
            await _takeRepository.AddTakeAsync(newTake);
            await _takeRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTakeById), new { id = newTake.Id }, newTake);

        }

    }
    
}