using Dropbox.Api.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using SmartaCam.App.Services;
using System.Data;
using System.Net.Http.Json;
using static Dropbox.Api.Paper.UserOnPaperDocFilter;

namespace SmartaCam.App.Pages
{
    public partial class TakesPage : ComponentBase
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

        protected int loadingState = 0;
        protected string bgUrl = string.Empty;
        protected string buttonBgColor = string.Empty;
        protected string recordButtonText = "Create New Recording";
        protected string recordButtonColor = "red";
        protected async override Task OnInitializedAsync()

        {
            //int loadingState = await TransportService.GetState();
            while (MyStateDisplay == 0 )
            {
                await Task.Delay(1000);
                MyStateDisplay = await TransportService.GetState();
            }
            AllTakes = await TakeService.GetAllTakesAsync();
            InitPlayStopLabelList();
			// Subscribe to the event
			_navigationManager.LocationChanged += LocationChanged;
			base.OnInitialized();
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
                Update_Background();
                ButtonText[id] = "Stop";
                ButtonColor[id] = "red";
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(async () => { await TransportService.PlayATake(id); });
                NowPlaying = await TransportService.NowPlaying();
                var waitTime = await TakeService.GetDurationById(id);
                while ( MyStateDisplay == 3 )
                    {
                    await Task.Delay(waitTime.Add(new TimeSpan(0,0,1)));
                    await CheckState();
                    }
                ButtonText[id] = "Play";
                ButtonColor[id] = "green";
                InitPlayStopLabelList();
                NowPlaying = null;
                //  await TransportService.StopButtonPress();



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
            if (MyStateDisplay < 2)
            {
                MyStateDisplay = 2;
                Update_Background();
                recordButtonText = "Stop Recording";
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
                recordButtonText = "Create New Recording";
                recordButtonColor = "red";
                NavigateToHome();

            }
        }
        public void EraseAll_Click()
        {
            TakeService.DeleteAllTakes();
			NavigateToHome();
		}
        protected async Task DeleteTake(int id)
        {
            await TakeService.DeleteTakeById(id);
            StatusClass = "alert-success";
            Message = "Deleted successfully";
            NavigateToHome(); 

        }
        void NavigateToHome()
        {
            _navigationManager.NavigateTo("/",true);
        }
        public string FormatPeakDecimal(decimal value)
        {
            return $"{value:F3}"; 

        }
        void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            string navigationMethod = e.IsNavigationIntercepted ? "HTML" : "code";
            //System.Diagnostics.Debug.WriteLine($"Notified of navigation via {navigationMethod} to {e.Location}");
            TransportService.StopButtonPress();
        }
        void IDisposable.Dispose()
        {
            // Unsubscribe from the event when our component is disposed
            _navigationManager.LocationChanged -= LocationChanged;
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

