using Microsoft.AspNetCore.Components;

namespace HERE.Blazor.APP.Pages
{
	public partial class Settings : TakesPage

    {
        [Inject]
        public ISettingsService SettingsService { get; set; }
        [Parameter]
        public bool Normalize { get; set; } = true;
		[Parameter]
		public bool Downmix { get; set; }
		[Parameter]
        public bool NormalizeSplitChannels { get; set; } = false;
        [Parameter]
        public bool CopyToUsb { get; set; } = false;
        [Parameter]
        public bool PushToCloud { get; set; } = false;
        [Parameter]
        public bool NetworkStatus { get; set; } = true;
		[Parameter]
		public bool DropBoxAuthStatus { get; set; } = true;
		[Parameter]
        public string DropBoxCode { get; set; } = string.Empty;
		[Parameter]

		public string SelectedRemovableDrive { get; set; } = string.Empty ;
		[Parameter]
        public string RemovableDrivePath { get; set; } = string.Empty;
        [Parameter]
        public List<string>? RemovableDrivePaths { get; set; }
		[Parameter]
		public string DropBoxAuthUrl { get; set; } = string.Empty;
        [Parameter]
        public bool DropBoxIsDisabled { get; set; } = false;
        [Parameter]
        public bool UsbIsDisabled { get; set; } = false;
		[Parameter]
		public bool DownmxIsDisabled { get; set; } = false;

		bool cloudauth = true;
        bool network = true;

        protected async override Task OnInitializedAsync()

        {
			while (MyStateDisplay == 0)
			{
				await Task.Delay(1000);
				MyStateDisplay = await TransportService.GetState();
			}
			
			Normalize = await SettingsService.GetNormalize();
			NormalizeSplitChannels = await SettingsService.GetNormalizeSplitChannels();
			PushToCloud = await SettingsService.GetUpload();
            CopyToUsb = await SettingsService.GetCopyToUsb();
            NetworkStatus = await SettingsService.GetNetworkStatus();
            DropBoxAuthStatus = await SettingsService.GetDropBoxAuthStatus();
			//DropBoxIsDisabled = DropBoxAuthStatus;
			RemovableDrivePath = await SettingsService.GetRemovableDrivePath();
			RemovableDrivePaths = await SettingsService.GetRemovableDrivePaths();
			Downmix = await SettingsService.GetDownmix();

			if (RemovableDrivePath == string.Empty) { UsbIsDisabled = true; }
            if (!DropBoxAuthStatus) { DropBoxIsDisabled = true; }
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
			SettingsService.SetNormalizeSplitChannels(NormalizeSplitChannels);
			SettingsService.SetDownmix(Downmix);
			SettingsService.SetUpload(PushToCloud);
            SettingsService.SetCopyToUsb(CopyToUsb);
          //  InvokeAsync(StateHasChanged);
        }
        public async Task AuthorizeDropBoxAsync()
        {
            await SettingsService.SetDropBoxCode(DropBoxCode);
			DropBoxAuthStatus = await SettingsService.GetDropBoxAuthStatus();
			DropBoxIsDisabled = true;
			PushToCloud = true;

			NavigateToSettings();
		}
		public void SetRemovableDrivePath()
		{
			SettingsService.SetRemovableDrivePath(RemovableDrivePath);
			InvokeAsync(StateHasChanged);
			NavigateToSettings();

		}
		protected async Task UpdateRemovableDrivePathFromDropdown(ChangeEventArgs e)
		{
			SelectedRemovableDrive = e.Value.ToString();
		}
		public async Task UnAuthorizeDropBoxAsync()
		{
			NavigateToSettings();
			DropBoxAuthStatus = false;
			PushToCloud = false;
			DropBoxIsDisabled = false;
			await SettingsService.UnAuthorizeDropBox();

		}
		void NavigateToSettings()
        {
            _navigationManager.NavigateTo("/settings", true);
        }

    }
}
