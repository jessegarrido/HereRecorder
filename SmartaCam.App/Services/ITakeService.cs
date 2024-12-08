using System.Text.Json;

namespace SmartaCam
{
    public interface ITakeService
    {

        public Task<List<Take>> GetAllTakesAsync();
        public Task<TimeSpan> GetDurationById(int id);

    }
}
