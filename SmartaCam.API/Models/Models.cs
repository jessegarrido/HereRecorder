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

	}
    public interface IMp3TagSetRepository
    {
        public Task<bool> SaveChangesAsync();
        public void AddMp3TagSet(Mp3TagSet mp3TagSet);
        public void SetActiveMp3TagSet(int mp3TagSetId);
        public Task<Mp3TagSet> GetMp3TagSetByIdAsync(int id);
        public Task<Mp3TagSet> GetActiveMp3TagSetAsync();
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
    }
    public class Mp3TagSetRepository : IMp3TagSetRepository
    {
        private readonly SmartaCamContext _context = new SmartaCamContext();
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public void AddMp3TagSet(Mp3TagSet mp3TagSet)
        {
            _context.Add<Mp3TagSet>(mp3TagSet);
            _context.SaveChanges();
        }
        public async Task<Mp3TagSet> GetActiveMp3TagSetAsync()
        {
            var mp3TagSetActive = _context.Mp3TagSets
                 .Where(t => t.IsDefault == true).FirstOrDefault();
            return mp3TagSetActive;
        }
        public void SetActiveMp3TagSet(int mp3TagSetId)
        {
            var mp3TagSetUnsetDefault = _context.Mp3TagSets
                 .Where(t => t.IsDefault == true).FirstOrDefault();
            mp3TagSetUnsetDefault.IsDefault = false;

            var mp3TagSetDefault = _context.Mp3TagSets
                .Where(t => t.Id == mp3TagSetId).FirstOrDefault();
            mp3TagSetDefault.IsDefault = true;
            _context.SaveChanges();
        }
        public async Task<Mp3TagSet> GetMp3TagSetByIdAsync(int id)
        {
            Mp3TagSet mp3TagSet = _context.Mp3TagSets
                .Where(e => (e.Id == id)).FirstOrDefault();
            return mp3TagSet;
        }
        public async Task<List<Mp3TagSet>> GetAllMp3TagSets()
        {
            List<Mp3TagSet> mp3TagSets = new();
            foreach (Mp3TagSet mp3TagSet in _context.Mp3TagSets)
                mp3TagSets.Add(mp3TagSet);
            return mp3TagSets;
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
           var folder = Environment.SpecialFolder.LocalApplicationData;
           var path = Environment.GetFolderPath(folder);
           DbPath = System.IO.Path.Join(path, "db.db");
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
                        Artist = "SmartaCam",
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
		public int Id { get; set; }
        //public int RunLengthInSeconds { get; set; }
        public float OriginalPeakVolume { get; set; } = 0;
        public string WavFilePath { get; set; } = string.Empty;
        public string Mp3FilePath { get; set; } = string.Empty;
        public bool Normalized { get; set; } = false;
        public bool WasConvertedToMp3 { get; set; } = false;
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Session { get; set; } = string.Empty;
        public bool WasUpLoaded { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
    }
    public class Mp3TagSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public bool IsDefault { get; set; }

        //  public string Content { get; set; }

        //  public int BlogId { get; set; }
        //  public Blog Blog { get; set; }
    }
}
