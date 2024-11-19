using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    public interface IMP3TagSetService
    {
        Task<Mp3TagSet> GetMp3TagSet(int id);
        Task AddMp3TagSet(Mp3TagSet mp3TagSet);
        Task<List<Mp3TagSet>> GetAllMp3TagSets();
    }

}
