using Microsoft.AspNetCore.Mvc;
using SmartaCam;

namespace SmartaCam
{
    public interface IMp3TagSetService
    {
        Task<Mp3TagSet> GetMp3TagSet(int id);
        Task AddMp3TagSet(Mp3TagSet mp3TagSet);
        Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSets();
    }

}
