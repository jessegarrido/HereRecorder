using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json;

namespace SmartaCam.App.Pages
{
    public class HomeBase : ComponentBase
    {
        //[Parameter]
        //public IEnumerable<Mp3TagSet> Mp3TagSets { get; set; }

    

        [Inject]
        public IMp3TagSetService Mp3TagSetService { get; set; }
        [Inject]
        public ITransportService TransportService { get; set; }
        [Inject]
        public ITakeService TakesService { get; set; }


        [Parameter]
        public int MyState { get; set; } = 0;
        [Parameter]
        public string NowPlaying { get; set; } = string.Empty;
        [Parameter]
        public IEnumerable<string>? PlayQueue { get; set; }

        //[CascadingParameter]
        //public Mp3TagSet ActiveMp3TagSet { get; set; } = new();
        [CascadingParameter]

        public List<Take> Takes { get; set; }
        [Parameter]
        public string NextTitle { get; set; } = string.Empty;
        [Parameter]

        public string NextSession { get; set; } = string.Empty;
        [Parameter]

        public string NextArtist { get; set; } = string.Empty;



        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;

        protected async override Task OnInitializedAsync()

        {
            //ActiveMp3TagSet = await Mp3TagSetService.GetActiveMp3TagSet();
            PlayQueue = await TransportService.PlayQueue();
           // NextTitle = ActiveMp3TagSet.Title;
           // NextSession = ActiveMp3TagSet.Album;
          //  NextArtist = ActiveMp3TagSet.Album;
            Takes = await TakesService.GetAllTakesAsync();
        }

        public void Record_Click()
        {
            MyState = 2;
            TransportService.RecordButtonPress();
            //Task.Delay(3000);

            //UpdateState_Click();
                
        }
        public async Task Play_Click()
        {
            MyState = 3;
            NowPlaying = await TransportService.PlayButtonPress();
            //Task.Delay(3000);       
            //Task.Delay(3000);
            // NowPlaying = await TransportService.NowPlaying();
            PlayQueue = await TransportService.PlayQueue();
        }
        public void Stop_Click()
        {
            TransportService.StopButtonPress();
            MyState = 1;
            // Task.Delay(3000);
            //UpdateState_Click();
        }
        public async Task UpdateState_Click()
        {

            MyState = await TransportService.GetState();
            InvokeAsync(StateHasChanged);
        }
    }
}