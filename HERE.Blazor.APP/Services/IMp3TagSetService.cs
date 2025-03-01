using Microsoft.AspNetCore.Mvc;
using HERE;

namespace HERE
{
    public interface IMp3TagSetService
    {
        public Task<Mp3TagSet> GetMp3TagSet(int id);
		public Task<int> AddMp3TagSet(Mp3TagSet mp3TagSet);
		public Task<List<Mp3TagSet>> GetAllMp3TagSets();
        public Task<IActionResult> DeleteMp3TagSet(int id);
        public Task<Mp3TagSet> SetActiveMp3TagSet(int id);
		public Task<Mp3TagSet> GetActiveMp3TagSet();

	}

}
