using Microsoft.AspNetCore.Components;
using SmartaCam.App.Services;
using System.Net.Http.Json;

namespace SmartaCam.App.Pages
{
    public class TakesBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<Take> Takes { get; set; }
        [Inject]
        public TakeService TakesService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Takes = (IEnumerable<Take>)await TakesService.GetAllTakesAsync();
        }

        public class Take
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Session { get; set; }

        }
    }
}

