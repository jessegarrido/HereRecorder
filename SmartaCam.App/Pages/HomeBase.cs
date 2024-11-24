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

        protected async override Task OnInitializedAsync()
        {
            Mp3TagSets = (await Mp3TagSetService.GetAllMp3TagSets()).ToList(); 
   
        }
        public void Record_Click()
        {
            TransportService.PlayRecordButtonPress();
        }
    }

}
