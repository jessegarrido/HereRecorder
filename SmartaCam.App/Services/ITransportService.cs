using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using SmartaCam;

namespace SmartaCam
{
    public interface ITransportService
    {
        //Task<Mp3TagSet> GetMp3TagSet(int id);
        //Task AddMp3TagSet(Mp3TagSet mp3TagSet);
        //Task<List<Mp3TagSet>> GetAllMp3TagSets();
        Task<IActionResult> PlayRecordButtonPress();
        Task<IActionResult> PlayButtonPress();
        Task<IActionResult> StopButtonPress();
        Task<IActionResult> SkipForwardButtonPress();
        Task<IActionResult> SkipBackButtonPress();
    }

}