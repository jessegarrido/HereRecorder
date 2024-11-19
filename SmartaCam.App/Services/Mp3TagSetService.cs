using Microsoft.AspNetCore.Mvc;

namespace SmartaCam
{
    public class Mp3TagSetService : IMP3TagSetService
    {
        private readonly HttpClient _httpClient;
        public Mp3TagSetService(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }
        public Task<Mp3TagSet> GetMp3TagSet(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Mp3TagSet> SetDefaultMp3TagSet(int id)
        {
            throw new NotImplementedException();
        }
        public Task AddMp3TagSet(Mp3TagSet mp3TagSet)
        {

            throw new NotImplementedException();
        }
           public Task<List<Mp3TagSet>> GetAllMp3TagSets()
        {

            throw new NotImplementedException();
        }
    }
}
