//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Forms;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Diagnostics.Eventing.Reader;
//using System.Net.Http.Json;
//using System.Text.Json;

//namespace SmartaCam.App.Pages
//{
//    public class HomeBase : ComponentBase
//    {
//        //[Parameter]
//        //public IEnumerable<Mp3TagSet> Mp3TagSets { get; set; }

    

//        [Inject]
//        public IMp3TagSetService Mp3TagSetService { get; set; }
//        [Inject]
//        public ITransportService TransportService { get; set; }
//        [Inject]
//        public ITakeService TakeService { get; set; }



//        //[CascadingParameter]
//        //public Mp3TagSet ActiveMp3TagSet { get; set; } = new();





//        protected string Message = string.Empty;
//        protected string StatusClass = string.Empty;

//        protected string bgUrl = string.Empty; 

//        protected async override Task OnInitializedAsync()

//        {
//            //ActiveMp3TagSet = await Mp3TagSetService.GetActiveMp3TagSet();
//            PlayQueue = await TransportService.PlayQueue();
//           // NextTitle = ActiveMp3TagSet.Title;
//           // NextSession = ActiveMp3TagSet.Album;
//          //  NextArtist = ActiveMp3TagSet.Album;

//         //   NowPlaying = PlayQueue.First();
//        }


//        public async Task Play_Click()
//        {
//            MyStateDisplay = 3;
//            Update_Background();

//            await TransportService.PlayButtonPress();
//          //  NowPlaying = await TransportService.NowPlaying();
//            InvokeAsync(StateHasChanged);
//            //Task.Delay(3000);       
//            //Task.Delay(3000);
//            PlayQueue = await TransportService.PlayQueue();
//          //  NowPlaying = await TransportService.NowPlaying();

//        }


//    }
//}