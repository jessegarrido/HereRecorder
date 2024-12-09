using Microsoft.AspNetCore.Components;
using static Dropbox.Api.Paper.UserOnPaperDocFilter;

namespace SmartaCam.App.Pages
{
    public partial class Settings

    {
        [Inject]
        public ISettingsService SettingsService { get; set; }
        [Parameter]
        public bool Normalize { get; set; } = true;
        [Parameter]
        public bool CopyToUsb { get; set; } = false;
        [Parameter]
        public bool PushToCloud { get; set; } = false;
        [Parameter]
        public bool NetworkStatus { get; set; } = true;

        bool cloudauth = true;
        bool network = true;
        private bool disabled = true;
        protected async override Task OnInitializedAsync()

        {
            Normalize = await SettingsService.GetNormalize();
            PushToCloud = await SettingsService.GetUpload();
            CopyToUsb = await SettingsService.GetCopyToUsb();
            NetworkStatus = await SettingsService.GetNetworkStatus();
        }
        public void OnSettingsChange()
        {
            SettingsService.SetNormalize(Normalize);
            SettingsService.SetUpload(PushToCloud);
            SettingsService.SetCopyToUsb(CopyToUsb);
            
            InvokeAsync(StateHasChanged);
        }

    }
}
