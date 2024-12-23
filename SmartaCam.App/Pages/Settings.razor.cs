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
		[Parameter]
		public bool DropBoxAuthStatus { get; set; } = true;
		[Parameter]
        public string? DropBoxCode { get; set; } = string.Empty;
		[Parameter]
		public string DropBoxAuthUrl { get; set; } = string.Empty;

		bool cloudauth = true;
        bool network = true;
        private bool disabled = true;
        protected async override Task OnInitializedAsync()

        {
            Normalize = await SettingsService.GetNormalize();
            PushToCloud = await SettingsService.GetUpload();
            CopyToUsb = await SettingsService.GetCopyToUsb();
            NetworkStatus = await SettingsService.GetNetworkStatus();
            DropBoxAuthStatus = await SettingsService.GetDropBoxAuthStatus();
			await InvokeAsync(StateHasChanged);
			DropBoxCode = await SettingsService.GetDropBoxCode();
            if (DropBoxCode.StartsWith("http"))
                {
                DropBoxAuthUrl = DropBoxCode;
                DropBoxCode = null;
				}
			

		}
        public void OnSettingsChange()
        {
            SettingsService.SetNormalize(Normalize);
            SettingsService.SetUpload(PushToCloud);
            SettingsService.SetCopyToUsb(CopyToUsb);
            
            InvokeAsync(StateHasChanged);
        }
        public async Task AuthorizeDropBoxAsync()
        {
            await SettingsService.SetDropBoxCode(DropBoxCode);
			DropBoxAuthStatus = await SettingsService.GetDropBoxAuthStatus();
            PushToCloud = true;
			NavigateToSettings();

		}
		public async Task UnAuthorizeDropBoxAsync()
		{
			await SettingsService.UnAuthorizeDropBox();
			NavigateToSettings();
			DropBoxAuthStatus = false;
			PushToCloud = false;

		}
        void NavigateToSettings()
        {
            _navigationManager.NavigateTo("/settings", true);
        }
    }
}
