using Dropbox.Api.Files;
using Microsoft.AspNetCore.Components;
using SmartaCam.App.Services;
using System.Data;
using System.Net.Http.Json;

namespace SmartaCam.App.Pages
{
    public partial class Takes : ComponentBase
    {
        [Inject]
        public ITakeService TakeService { get; set; }
        [Inject]
        public ITransportService TransportService { get; set; }
        [Parameter]
        public Dictionary<int, string> ButtonText { get; set; } = new();
        public Dictionary<int, string> ButtonColor { get; set; } = new();
        [Parameter]
        public List<Take> AllTakes { get; set; }
        [Parameter]
        public string? NowPlaying { get; set; } = string.Empty;

        [Parameter]
        public int MyStateDisplay { get; set; } = 0;

        [Parameter]
        public List<string>? PlayQueue { get; set; }
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;

        protected string bgUrl = string.Empty;
        protected string buttonBgColor = string.Empty;
        protected string recordButtonText = "New Recording";
        protected string recordButtonColor = "red";
        protected async override Task OnInitializedAsync()

        {
            AllTakes = await TakeService.GetAllTakesAsync();
            InitPlayStopLabelList();
        }
        public async Task PlayStop_Click(int id)
        {
            await CheckState();
            if (MyStateDisplay == 3) //playing
            {
                await TransportService.StopButtonPress();
                MyStateDisplay = 1;
                ButtonText[id] = "Play";
                ButtonColor[id] = "green";
                
                InitPlayStopLabelList();
                NowPlaying = null;
            }
            else if (MyStateDisplay != 2)  //not recording
            {
                MyStateDisplay = 3;
                ButtonText[id] = "Stop";
                ButtonColor[id] = "red";
                await TransportService.PlayATake(id);
                NowPlaying = await TransportService.NowPlaying();
            }
            Update_Background();
        }
        public void InitPlayStopLabelList()
        {
            foreach (Take take in AllTakes)
            {
                if (ButtonText.ContainsKey(take.Id))
                {
                   ButtonText[take.Id] = "Play";
                   ButtonColor[take.Id] = "green";
                }
                else
                {
                   ButtonText.Add(take.Id, "Play");
                   ButtonColor.Add(take.Id, "green");
                }
                
            }
        }
        public async Task RecordStop_Click()
        {
            MyStateDisplay = await TransportService.GetState();
            if (MyStateDisplay == 1)
            {
                MyStateDisplay = 2;
                Update_Background();
                recordButtonText = "Stop";
                recordButtonColor = "white";
                _ = Task.Run ( async () => { await TransportService.RecordButtonPress(); });
            } else
            {
                await TransportService.StopButtonPress();
                MyStateDisplay = 1;
                Update_Background();
                InitPlayStopLabelList();
                NowPlaying = null;
               // await StopAudio();
                recordButtonText = "New Recording";
                recordButtonColor = "red";
            }
        }
        //public async Task Stop_Click()
        //{
        //    MyStateDisplay = await TransportService.GetState();
        //    if (MyStateDisplay != 1)
        //    {
        //        await CheckState();
        //        MyStateDisplay = 1;
        //        Update_Background();
        //        InitPlayStopLabelList();
        //        NowPlaying = null;
        //    }
        //}
        public async Task CheckState()
        {
            MyStateDisplay = await TransportService.GetState();
           // InvokeAsync(StateHasChanged);
        }
        public void Update_Background()
        {
            switch (MyStateDisplay)
            {
                case 2:
                    bgUrl = "red.jpg";
                    break;
                case 3:
                    bgUrl = "green.jpg";
                    break;
                default:
                    bgUrl = "white.png";
                    break;
            }

        }


    }
}

