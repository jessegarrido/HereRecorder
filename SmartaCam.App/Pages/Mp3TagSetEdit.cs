using Dropbox.Api.TeamPolicies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartaCam.App.Pages
{
    public partial class Mp3TagSetEditBase
    {
        [Inject]
        public IMp3TagSetService Mp3TagSetService { get; set; }
        [Inject]
        public ITransportService TransportService { get; set; }

        [Parameter]
        public IEnumerable<Mp3TagSet> Mp3TagSets { get; set; }

        [Parameter]
        public Mp3TagSet ActiveMp3TagSet { get; set; } = new();
        [Parameter]
        public Mp3TagSet CandidateMp3TagSet { get; set; } = new();
        //[Parameter]
        //public string TranslatedTitle { get; set; } = string.Empty;
        //[Parameter]
        //public string TranslatedSession { get; set; } = string.Empty;
        [Parameter]
        public string CandidateTitle { get; set; } = string.Empty;
        [Parameter]

        public string CandidateAlbum { get; set; } = string.Empty;
        [Parameter]

        public string CandidateArtist { get; set; } = string.Empty;

        public InputText TitleInputText { get; set; }

        public InputText AlbumInputText { get; set; }
        public InputText ArtistInputText { get; set; }



        protected int Mp3TagSetId = 0;
        protected bool Saved;
        protected string StatusClass = string.Empty;
        protected async override Task OnInitializedAsync()

        {
            Saved = false;
            ActiveMp3TagSet = await Mp3TagSetService.GetActiveMp3TagSet();
            //CandidateMp3TagSet = ActiveMp3TagSet;
            Mp3TagSets = await Mp3TagSetService.GetAllMp3TagSets();
           CandidateTitle = ActiveMp3TagSet.Title;
           CandidateArtist = ActiveMp3TagSet.Artist;
           CandidateAlbum = ActiveMp3TagSet.Album;
           // TranslatedTitle = NextTitle.TranslateString();
           // TranslatedSession = NextSession.TranslateString();
           //  TranslatedTitle = EditTitle.Replace("[Date]", DateTime.Now.ToString());
        }

        protected async Task HandleValidSubmit()
        {
            //   await Mp3TagSetService.AddMp3TagSet(ActiveMp3TagSet);
          //  CandidateMp3TagSet.Id = int.Parse(Mp3TagSetId);
            if (
                    (CandidateTitle != ActiveMp3TagSet.Title ) ||

                    (CandidateArtist != ActiveMp3TagSet.Artist) ||
                    (CandidateAlbum != ActiveMp3TagSet.Album) 
               ) //new
            {
                Mp3TagSet newMp3TagSet = new();
                //newMp3TagSet.Id = 0;
                newMp3TagSet.Title = CandidateTitle;
                newMp3TagSet.Album = CandidateAlbum;
                newMp3TagSet.Artist = CandidateArtist;

                var addedMp3TagSet = await Mp3TagSetService.AddMp3TagSet(newMp3TagSet);
                if (addedMp3TagSet != null)
                {
                    StatusClass = "alert-success";
                    Message = "New Mp3 Tag Template saved.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "That Mp3 tag template already exists";
                    Saved = false;
                }
            }
            //else
            //{
            //    await EmployeeDataService.UpdateEmployee(Employee);
            //    StatusClass = "alert-success";
            //    Message = "Employee updated successfully.";
            //    Saved = true;
            //}
        }
        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteMp3TagSet()
        {
            await Mp3TagSetService.DeleteMp3TagSet(ActiveMp3TagSet.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }
        protected async Task UpdateActiveMp3TagSet()
        {
            if (Mp3TagSetId != 0)
            {
                await Mp3TagSetService.SetActiveMp3TagSet(Mp3TagSetId);
                CandidateMp3TagSet = await Mp3TagSetService.GetActiveMp3TagSet();
                CandidateTitle = CandidateMp3TagSet.Title;
                CandidateArtist = CandidateMp3TagSet.Artist;
                CandidateAlbum = CandidateMp3TagSet.Album;
            }
        }
        public Task OnValueChanged(int value)
        {
            Mp3TagSetId = value;
            return Task.CompletedTask;
        }
    }
}