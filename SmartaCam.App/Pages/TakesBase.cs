using Dropbox.Api.Files;
using Microsoft.AspNetCore.Components;
using SmartaCam.App.Services;
using System.Net.Http.Json;

namespace SmartaCam.App.Pages
{
    public partial class TakesBase
    {
        [Inject]
        public TakeService TakesService { get; set; }

      //  [CascadingParameter]
       // public List<Take> ComponentTakes { get; set; } = new();

            //public class Take
            //{
            //    public int Id { get; set; }
            //    public string Title { get; set; }
            //    public string Artist { get; set; }
            //    public string Session { get; set; }

            //}
    }
}

