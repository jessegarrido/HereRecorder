using Microsoft.AspNetCore.Components;

namespace SmartaCam.App.Pages
{
    public class Mp3TagSetDetailBase : ComponentBase
    {
        [Inject]
        public required IMp3TagSetService Mp3TagSetService { get; set; }
        [Parameter]
        public Mp3TagSet Mp3TagSet { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Mp3TagSet = await Mp3TagSetService.GetMp3TagSet(1);

        }
    }
}
