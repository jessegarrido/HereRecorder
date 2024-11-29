using Microsoft.AspNetCore.Mvc;
using SmartaCam;

namespace SmartaCam
{
    public interface IMp3TagSetService
    {
        public Task<Mp3TagSet> GetMp3TagSet(int id);
		public Task<Mp3TagSet> AddMp3TagSet(Mp3TagSet mp3TagSet);
		public Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSets();
        public Task<Mp3TagSet> GetActiveMp3TagSet();
        public Task<IActionResult> DeleteMp3TagSet(int id);
        public Task<IActionResult> SetActiveMp3TagSet(int id);

    }

}
