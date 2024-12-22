using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace SmartaCam.App.Pages
{
	public partial class TagEditor
    {
        [Inject]
        public IMp3TagSetService Mp3TagSetService { get; set; }
        [Inject]
        public ITransportService TransportService { get; set; }

        [Parameter]
        public List<Mp3TagSet> Mp3TagSets { get; set; }

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

        //private int _selectedMp3TagSetId = 1;
        [Parameter]

        public int SelectedMp3TagSetId { get; set; } = 1;
        [Parameter]
        public bool ShowDeleteButton { get; set; } = true;
        //{
        //    get
        //    {
        //        return _selectedMp3TagSetId;
        //    }
        //    set
        //    {
        //            if (_selectedMp3TagSetId != value)
        //            {
        //                _selectedMp3TagSetId = value;
        //                ActiveMp3TagSet = Mp3TagSetService.SetActiveMp3TagSet(value);
        //              //  ActiveMp3TagSet = setActiveTask;
        //                CandidateTitle = ActiveMp3TagSet.Title;
        //                CandidateArtist = ActiveMp3TagSet.Artist;
        //                CandidateAlbum = ActiveMp3TagSet.Album;

        //                UpdateActiveMp3TagFromDropdown(value);
        //            }
        //        }
        //    } 
        //}

        public InputText TitleInputText { get; set; }

        public InputText AlbumInputText { get; set; }
        public InputText ArtistInputText { get; set; }


        //protected int Mp3TagSetId = 0;
        protected bool Saved;
        protected string StatusClass = string.Empty;
        // private IEnumerable<Mp3TagSet> Mp3TagSetEnum;
        protected string Message = string.Empty;

        protected async override Task OnInitializedAsync()

        {
            Saved = false;
            Mp3TagSets = await Mp3TagSetService.GetAllMp3TagSets();
            //// = (List<Mp3TagSet>)Mp3TagSetEnum;
            ActiveMp3TagSet = await Mp3TagSetService.GetActiveMp3TagSet();



            ////CandidateMp3TagSet.Id = 0;
            SelectedMp3TagSetId = ActiveMp3TagSet.Id;   

                    CandidateTitle = ActiveMp3TagSet.Title;
                    CandidateArtist = ActiveMp3TagSet.Artist;
                    CandidateAlbum = ActiveMp3TagSet.Album;
                    ShowHideDeleteButton();

            //         // TranslatedTitle = NextTitle.TranslateString();
            //         // TranslatedSession = NextSession.TranslateString();
            //         //  TranslatedTitle = EditTitle.Replace("[Date]", DateTime.Now.ToString());
           }

            protected async Task HandleValidSubmit()
            {
                //   await Mp3TagSetService.AddMp3TagSet(ActiveMp3TagSet);
                //  CandidateMp3TagSet.Id = int.Parse(Mp3TagSetId);
                if (
                        (CandidateTitle != ActiveMp3TagSet.Title) ||

                        (CandidateArtist != ActiveMp3TagSet.Artist) ||
                        (CandidateAlbum != ActiveMp3TagSet.Album)
                   ) //new
                {
                    Mp3TagSet newMp3TagSet = new();
                    // newMp3TagSet.Id = 0;
                    newMp3TagSet.Title = CandidateTitle;
                    newMp3TagSet.Album = CandidateAlbum;
                    newMp3TagSet.Artist = CandidateArtist;
                    //newMp3TagSet.IsDefault = true;

                    int addedMp3TagSetId = await Mp3TagSetService.AddMp3TagSet(newMp3TagSet);
                    Console.WriteLine(addedMp3TagSetId);
                    if (addedMp3TagSetId != 0)
                    {
                        StatusClass = "alert-success";
                        //  Message = "New Mp3 Tag Template saved.";
                        Saved = true;
                        var ActiveMp3TagSet = await Mp3TagSetService.SetActiveMp3TagSet(addedMp3TagSetId);
                        CandidateTitle = ActiveMp3TagSet.Title;
                        CandidateArtist = ActiveMp3TagSet.Artist;
                        CandidateAlbum = ActiveMp3TagSet.Album;
                        ShowHideDeleteButton();
                    }
                    else
                    {
                        StatusClass = "alert-danger";
                        Message = "Could not save new Mp3 tag template";
                        Saved = false;
                    }
                    Mp3TagSets = await Mp3TagSetService.GetAllMp3TagSets();
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
                await Mp3TagSetService.DeleteMp3TagSet(SelectedMp3TagSetId);
                StatusClass = "alert-success";
                Message = "Deleted successfully";

                Saved = true;
                ShowDeleteButton = false;

                Mp3TagSets = await Mp3TagSetService.GetAllMp3TagSets();
                ActiveMp3TagSet = await Mp3TagSetService.SetActiveMp3TagSet(1);
            }
            protected void ShowHideDeleteButton()
            {
                if (
                    ActiveMp3TagSet.Id > 1 &&
                    CandidateTitle == ActiveMp3TagSet.Title &&
                    CandidateArtist == ActiveMp3TagSet.Artist &&
                    CandidateArtist == ActiveMp3TagSet.Album)
                {
                    ShowDeleteButton = true;
                }
                else
                {
                    ShowDeleteButton = false;
                }
            }

            protected async Task UpdateActiveMp3TagFromDropdown(ChangeEventArgs e)
            {
                SelectedMp3TagSetId = int.Parse(e.Value.ToString());
                if (SelectedMp3TagSetId > 0)
                {
                    ActiveMp3TagSet = await Mp3TagSetService.SetActiveMp3TagSet(SelectedMp3TagSetId);
                }
                CandidateTitle = ActiveMp3TagSet.Title;
                CandidateArtist = ActiveMp3TagSet.Artist;
                CandidateAlbum = ActiveMp3TagSet.Album;
                if (SelectedMp3TagSetId < 2)
                {
                    ShowDeleteButton = false;
                }
                else
                {
                    ShowDeleteButton = true;
                }
            }


            //public Task OnValueChanged(int value)
            //{
            //    Mp3TagSetId = value;
            //    return Task.CompletedTask;
            //}
        }
    }