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

namespace SmartaCam
{
    public interface IWavTakeRepository
    {
        public void AddWavTake(WavTake wavTake);
        public void MarkNormalized(int wavTakeId);
    }
    public interface IMp3TakeRepository
    {
        public void AddMp3Take(Mp3Take mp3Take);
        public void MarkUploaded(int mp3TakeId);
    }
    public interface IMp3TagSetRepository
    {
        public void AddMp3TagSet(Mp3TagSet mp3TagSet);
        public void SetDefaultMp3TagSet(int mp3TagSetId);
        public Task<Mp3TagSet> GetMp3TagSetByIdAsync(int id);
    }
    public class WavTakeRepository : IWavTakeRepository
    {

        private readonly TakeContext _context = new TakeContext();
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public void AddWavTake(WavTake wavTake)
        {
            _context.Add<WavTake>(wavTake);
            _context.SaveChanges();
        }
        public WavTake GetWavTakeById(int id)
        {
            WavTake wavTake = _context.WavTakes
                .Where(e => String.Equals(e.WavTakeId, id))
                .FirstOrDefault();
            return wavTake;
        }
        public List<WavTake> GetAllWavTakes()
        {
            List<WavTake> wavTakes = new();
            foreach (WavTake wavTake in _context.WavTakes)
                wavTakes.Add(wavTake);
            return wavTakes;
        }
        public void MarkNormalized(int wavTakeId)
        {
            var wavTake = _context.WavTakes
                .Where(t => t.WavTakeId == wavTakeId).FirstOrDefault();
            wavTake.IsNormalized = true;
            _context.SaveChanges();
        }
    }
    public class Mp3TakeRepository : IMp3TakeRepository
    {
        private readonly TakeContext _context = new TakeContext();
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public void AddMp3Take(Mp3Take mp3Take)
        {
            _context.Add<Mp3Take>(mp3Take);
            _context.SaveChanges();
        }

        public Mp3Take GetMp3TakeById(int id)
        {
            Mp3Take mp3Take = _context.Mp3Takes
                .Where(e => e.Mp3TakeId == id)
                .FirstOrDefault();
            return mp3Take;
        }
        public List<Mp3Take> GetAllMp3Takes()
        {
            List<Mp3Take> mp3Takes = new();
            foreach (Mp3Take mp3Take in _context.Mp3Takes)
                mp3Takes.Add(mp3Take);
            return mp3Takes;
        }
        public void MarkUploaded(int mp3TakeId)
        {
            var mp3Take = _context.Mp3Takes
                .Where(t => t.Mp3TakeId == mp3TakeId).FirstOrDefault();
            mp3Take.IsUpLoaded = true;
            _context.SaveChanges();
        }
    }
    public class Mp3TagSetRepository : IMp3TagSetRepository
    {
        private readonly TakeContext _context = new TakeContext();
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public void AddMp3TagSet(Mp3TagSet mp3TagSet)
        {
            _context.Add<Mp3TagSet>(mp3TagSet);
            _context.SaveChanges();
        }
        public void SetDefaultMp3TagSet(int mp3TagSetId)
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
        public List<Mp3TagSet> GetAllMp3TagSets()
        {
            List<Mp3TagSet> mp3TagSets = new();
            foreach (Mp3TagSet mp3TagSet in _context.Mp3TagSets)
                mp3TagSets.Add(mp3TagSet);
            return mp3TagSets;
        }
    }
    public class TakeContext : DbContext
    {
        public DbSet<WavTake> WavTakes { get; set; }
        public DbSet<Mp3Take> Mp3Takes { get; set; }
        public DbSet<Mp3TagSet> Mp3TagSets { get; set; }
        public string DbPath { get; }
        public TakeContext()
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
    public class WavTake
    {
        public int WavTakeId { get; set; }
        public int RunLengthInSeconds { get; set; }
        public decimal OriginalPeakVolume { get; set; }
        public string FileName { get; set; } = string.Empty;
        public bool IsNormalized { get; set; }
        public bool WasConvertedToMp3 { get; set; }
        // public int CreationDate { get; set; }

        //public string Url { get; set; }

        // public WavTakeI <Post> Posts { get; } = new();
    }

    public class Mp3Take
    {
        public int Mp3TakeId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public bool IsUpLoaded { get; set; }
        // public int CreationDate { get; set; }
        //  public string Content { get; set; }

        //  public int BlogId { get; set; }
        //  public Blog Blog { get; set; }
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
