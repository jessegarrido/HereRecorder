using System.Text.Json;

namespace SmartaCam
{
    public interface ITakeService
    {

        public Task<IEnumerable<Take>> GetAllTakesAsync();

    }
}
