using Microsoft.AspNetCore.Components;

namespace SmartaCam.App.Pages
{
    public class HomeBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<Mp3TagSet> Mp3TagSets {  get; set; }
        [Inject]
        public IMp3TagSetService Mp3TagSetService { get; set; }
        [Inject]
        public ITransportService TransportService { get; set; }
        [Parameter]
        public int StateValue { get; set; }
        [Parameter]
        public EventCallback<int> StateValueChanged { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Mp3TagSets = (await Mp3TagSetService.GetAllMp3TagSets()).ToList(); 
   
        }

        public void Record_Click()
        {
            TransportService.RecordButtonPress();
        }
        public void Play_Click()
        {
            TransportService.PlayButtonPress();
        }
        public void Stop_Click()
        {
            TransportService.StopButtonPress();
        }
        public async Task UpdateStateValue()
        {
           StateValue = TransportService.GetState().Result;
           await StateValueChanged.InvokeAsync(StateValue);


        }
    }

}
