using Microsoft.AspNetCore.Mvc;
using SmartaCam;

namespace SmartaCam
{
    public interface IMp3TagSetService
    {
        public Task<Mp3TagSet> GetMp3TagSet(int id);
		public Task AddMp3TagSet(Mp3TagSet mp3TagSet);
		public Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSets();
    }

}
