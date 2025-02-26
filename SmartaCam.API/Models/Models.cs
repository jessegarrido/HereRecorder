using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartaCam;
using Microsoft.AspNetCore.Mvc;
using SmartaCam.API;

namespace SmartaCam
{



    public interface ITakeRepository
    {
        public Task<bool> SaveChangesAsync();
        public Task AddTakeAsync(Take take);
        public Task<List<Take>> GetAllTakesAsync();
        public void MarkNormalized(int id);

        public void MarkMp3Created(int id);
        public void MarkUploaded(int id);
        public Task<Take> GetTakeByIdAsync(int id);
        public Task<DateTime> GetLastTakeDateAsync();
        public Task<string> GetTakeFilePathByIdAsync(int id);
        public Task<TimeSpan> GetTakeDurationByIdAsync(int id);
        public Task DeleteTakeByIdAsync(int id);
		public Task ResetTakesTableAsync();
        public Task DeleteAllTakesAsync();
	}
    public class TakeRepository : ITakeRepository
    {

        private readonly SmartaCamContext _context = new SmartaCamContext();
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task AddTakeAsync(Take take)
        {
            _context.Add<Take>(take);
            _context.SaveChanges();
        }
        public async Task<Take> GetTakeByIdAsync(int id)
        {
            Take take = _context.Takes
                .Where(e => String.Equals(e.Id, id))
                .FirstOrDefault();
            return take;
        }

        public async Task<string> GetTakeFilePathByIdAsync(int id)
        {
            Take take = _context.Takes
                .Where(e => String.Equals(e.Id, id))
                .FirstOrDefault();
            return take.WavFilePath;
        }
        public async Task<DateTime> GetLastTakeDateAsync()
		{
            DateTime latest = _context.Takes
                .Max(d => d.Created);
            return latest;
		}
		public async Task<List<Take>> GetAllTakesAsync()
        {
            List<Take> takes = new();
            foreach (Take take in _context.Takes)
                takes.Add(take);
            return takes;
        }
        public async Task<TimeSpan> GetTakeDurationByIdAsync(int id)
        {
            Take take = _context.Takes
                           .Where(e => String.Equals(e.Id, id))
                           .FirstOrDefault();
            return take.Duration;
        }
        public void MarkNormalized(int TakeId)
        {
            var take = _context.Takes
                .Where(t => t.Id == TakeId).FirstOrDefault();
            take.Normalized = true;
            _context.SaveChanges();
        }
        public void MarkMp3Created(int TakeId)
       {
         var take = _context.Takes
                .Where(t => t.Id == TakeId).FirstOrDefault();
            take.WasConvertedToMp3 = true;
            _context.SaveChanges();
        }

        public void MarkUploaded(int takeId)
        {
        var take = _context.Takes
                    .Where(t => t.Id == takeId).FirstOrDefault();
                take.WasUpLoaded = true;
                _context.SaveChanges();
        }
        public async Task DeleteTakeByIdAsync(int id)
        {
            {
                Console.WriteLine($"delete id {id}");
                Take takeToDelete = _context.Takes
                   .Where(e => e.Id == id).FirstOrDefault();
                _context.Remove(takeToDelete);
                _context.SaveChanges();
            }
        }
		public async Task DeleteAllTakesAsync()
		{
			string[] allfiles = Directory.GetFiles(Config.LocalRecordingsFolder, "*.*", SearchOption.AllDirectories);
            if (allfiles.Length > 0)
            {
                UIRepository uiRepository = new();
                uiRepository.DeleteAllRecordings(allfiles);
                await ResetTakesTableAsync();
            }
		}
		public async Task ResetTakesTableAsync()
		{

			Console.WriteLine($"Resetting Takes Database");
             await _context.Takes.ExecuteDeleteAsync();
			//_context.SaveChanges();
			//_context.Database.Migrate();

		}
	}

	public interface IMp3TagSetRepository
	{
		public Task<bool> SaveChangesAsync();
		public Task<int> AddMp3TagSetAsync(Mp3TagSet mp3TagSet);
		public Task<Mp3TagSet> SetActiveMp3TagSetAsync(int id);
		public Task<Mp3TagSet> GetMp3TagSetByIdAsync(int id);
		public Task<Mp3TagSet> GetActiveMp3TagSetAsync();
		public Task DeleteMp3TagSetByIdAsync(int id);
		public Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSetsAsync();
		public Task<bool> CheckIfMp3TagSetExistsAsync(Mp3TagSet mp3TagSet);
	}
	public class Mp3TagSetRepository : IMp3TagSetRepository
    {
        private readonly SmartaCamContext _context = new SmartaCamContext();
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<bool> CheckIfMp3TagSetExistsAsync(Mp3TagSet mp3TagSet)
        {
            Mp3TagSet existingTagSet = _context.Mp3TagSets
                    .Where(m => m.Title == mp3TagSet.Title &&
                                m.Artist == mp3TagSet.Artist &&
                                m.Album == mp3TagSet.Album).FirstOrDefault();
            return (existingTagSet != null);
        }
            public async Task<int> AddMp3TagSetAsync(Mp3TagSet mp3TagSet)
            { 
            var existingTagSet = _context.Mp3TagSets
                    .Where(m => m.Title == mp3TagSet.Title &&
                                m.Artist == mp3TagSet.Artist &&
                                 m.Album == mp3TagSet.Album).FirstOrDefault();
            if (existingTagSet == null)
            {
                var addedEntity = _context.Add<Mp3TagSet>(mp3TagSet);
                _context.SaveChanges();
                int newId = mp3TagSet.Id;
                return newId;
            }
            else return existingTagSet.Id;
        }
        public async Task<Mp3TagSet> GetActiveMp3TagSetAsync()
        {
            var mp3TagSetActive = _context.Mp3TagSets
                 .Where(m => m.IsDefault == true).FirstOrDefault();
            return mp3TagSetActive;
        }
        public async Task<Mp3TagSet> SetActiveMp3TagSetAsync(int mp3TagSetId)
        {
            foreach (var mp3TagSet in _context.Mp3TagSets.ToList())
                {
                if (mp3TagSet.Id != mp3TagSetId)
                    { 
                        //Console.WriteLine(mp3TagSet.Id);
                        mp3TagSet.IsDefault = false;
                    }
                }
            Console.WriteLine(mp3TagSetId);
            

            var mp3TagSet2 = _context.Mp3TagSets
                .Where(m => m.Id == mp3TagSetId).FirstOrDefault();
            mp3TagSet2.IsDefault = true;
            _context.SaveChanges();

            return await GetActiveMp3TagSetAsync();
        }
        public async Task<Mp3TagSet> GetMp3TagSetByIdAsync(int id)
        {
            Mp3TagSet mp3TagSet = _context.Mp3TagSets
                .Where(e => (e.Id == id)).FirstOrDefault();
            return mp3TagSet;
        }
        public async Task<IEnumerable<Mp3TagSet>> GetAllMp3TagSetsAsync()
        {
            List<Mp3TagSet> mp3TagSets = new();
            foreach (Mp3TagSet mp3TagSet in _context.Mp3TagSets)
                mp3TagSets.Add(mp3TagSet);
            return mp3TagSets;
        }
        public async Task DeleteMp3TagSetByIdAsync(int id)
        {
            {
                Console.WriteLine($"delete id {id}");
                Mp3TagSet mp3TagSetToDelete = _context.Mp3TagSets
                   .Where(e => e.Id == id).FirstOrDefault();
                _context.Remove(mp3TagSetToDelete);
                _context.SaveChanges();
            }
        }
        public void TranslateMp3TagField(string unsubstitutedTagSet, string session)
        {
            //var session = DateTime.Today == null ? "UNKNOWN" : DateTime.Today.ToString("yyyy-MM-dd");
            var substitutedTag = unsubstitutedTagSet.Replace("[Date]", session);
            substitutedTag = substitutedTag.Replace("[#]", Settings.Default.Takes.ToString());
        }

    }
	public interface ISettingsRepository
	{
		public Task<bool> GetDownmixAsync();
		public Task SetDownmixAsync(bool toMono);
		public Task<bool> GetNormalizeAsync();
		public Task SetNormalizeAsync(bool willNormalize);
		public Task<bool> GetNormalizeSplitChannelsAsync();
		public Task SetNormalizeSplitChannelsAsync(bool splitChannels);
		public Task<bool> GetUploadAsync();
		public Task SetUploadAsync(bool willUpload);
		public Task<bool?> GetCopyToUsbAsync();
		public Task SetCopyToUsbAsync(bool willCopy);
		public Task<bool> GetNetworkStatus();
		public Task<string> GetDropBoxCode();
		public Task SetDropBoxCode(string dropboxCode);
		public Task<bool> GetDropBoxAuthStatusAsync();
		public Task UnAuthDropBoxAsync();
		public Task<string?> GetRemovableDrivePathAsync();
		public Task SetRemovableDrivePathAsync(string removableDrivePath);
		public Task<List<string>>? GetRemovableDrivePathsAsync();
		//public Task CheckAuthentication();
		//public Task SetLocalRecordingsFolder();
		//public Task<bool> CheckNetworkStatus();

	}
	public class SettingsRepository : ISettingsRepository
	{
		public async Task<bool> GetDownmixAsync()
		{
			return Config.DownmixToMono;
		}
		public async Task SetDownmixAsync(bool toMono)
		{
			Config.DownmixToMono = toMono;
			Settings.Default.DownmixToMono = toMono.ToString();
			Settings.Default.Save();
		}
		public async Task<bool> GetNormalizeAsync()
		{
			return Config.Normalize;
		}
		public async Task SetNormalizeAsync(bool willNormalize)
		{
			Config.Normalize = willNormalize;
			Settings.Default.Normalize = willNormalize.ToString();
			Settings.Default.Save();
		}
		public async Task<bool> GetNormalizeSplitChannelsAsync()
		{
			return Config.NormalizeSplitChannels;
		}
		public async Task SetNormalizeSplitChannelsAsync(bool splitChannels)
		{
			Config.NormalizeSplitChannels = splitChannels;
			Settings.Default.NormalizeSplitChannels = splitChannels.ToString();
			Settings.Default.Save();
		}
		public async Task<bool> GetUploadAsync()
		{
			return Config.PushToCloud;
		}
		public async Task SetUploadAsync(bool willUpload)
		{
			Config.PushToCloud = willUpload;
			Settings.Default.PushToCloud = willUpload.ToString();
			Settings.Default.Save();
		}
		public async Task<bool?> GetCopyToUsbAsync()
		{
			return Config.CopyToUsb;
		}
		public async Task SetCopyToUsbAsync(bool willCopy)
		{
			Config.CopyToUsb = willCopy;
			Settings.Default.PushToCloud = willCopy.ToString();
			Settings.Default.Save();
		}
		public async Task<bool> GetNetworkStatus()
		{
			NetworkRepository networkRepo = new();
			return networkRepo.GetNetworkStatus();
		}
		public async Task<string> GetDropBoxCode()
		{
			//NetworkRepository networkRepo = new();
			return Config.DropBoxCodeTxt;
		}
		public async Task<string?> GetRemovableDrivePathAsync()
		{
			return Config.RemovableDrivePath;
		}
		public async Task SetRemovableDrivePathAsync(string removableDrivePath)
		{
			Config.RemovableDrivePath = removableDrivePath;
			Settings.Default.RemovableDrivePath = removableDrivePath;
			Settings.Default.Save();
		}
		public async Task<List<string>>? GetRemovableDrivePathsAsync()
		{
			return Config.RemovableDrivePaths;
		}
		public async Task<bool> GetDropBoxAuthStatusAsync()
		{
			return NetworkRepository.OAuthStatus;
		}

		public async Task SetDropBoxCode(string dropboxCode)
		{
			//NetworkRepository networkRepo = new();
			Config.DropBoxCodeTxt = dropboxCode;
			//await networkRepo.CheckAndConnectCloudAsync();
		}
		public async Task UnAuthDropBoxAsync()
		{
			//NetworkRepository networkRepository = new();
			NetworkRepository.DropBox db = new();
			await db.DropBoxAuthResetAsync();
			NetworkRepository.OAuthStatus = false;
			await db.DropBoxAuth();
		}
	}
	public class SmartaCamContext : DbContext
    {
        public DbSet<Take> Takes { get; set; }
        //public DbSet<Mp3Take> Mp3Takes { get; set; }
        public DbSet<Mp3TagSet> Mp3TagSets { get; set; }
        public string DbPath { get; }
        public SmartaCamContext()
        {
           // var folder = Environment.SpecialFolder.LocalApplicationData;
            var folder = Environment.SpecialFolder.Personal;
            var path = Environment.GetFolderPath(folder);
           // DbPath = System.IO.Path.Join(path, "db.db");
            DbPath = System.IO.Path.Combine(path,"Here","db.db");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mp3TagSet>(b =>
            {
                b.Property(x => x.Title).IsRequired();
                b.HasData(
                    new Mp3TagSet
                    {
                        Id = 1,
                        Title = "[Date]_take-[#]",
                        Artist = "H E R E",
                        Album = "[Date]",
                        IsDefault = true
                    }
                );
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options
            .UseSqlite($"Data Source={DbPath}");
    }
    public class Take
    {
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public int RunLengthInSeconds { get; set; }
        public string OriginalPeakVolume { get; set; } = string.Empty;
        public float ChannelOneInputPeak { get; set; } = 0;
        public float ChannelTwoInputPeak { get; set; } = 0;
        public string WavFilePath { get; set; } = string.Empty;
        public bool IsMono { get; set; } = false;
        public string Mp3FilePath { get; set; } = string.Empty;
        public bool Normalized { get; set; } = false;
        public bool WasConvertedToMp3 { get; set; } = false;
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Session { get; set; } = string.Empty;
        public bool WasUpLoaded { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; }
    }
    public class Mp3TagSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       // [RegularExpression(@"#", ErrorMessage = "Title Must Contain #")] Cannot make this work wtf
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = true;

        //  public string Content { get; set; }

        //  public int BlogId { get; set; }
        //  public Blog Blog { get; set; }
    }
}
