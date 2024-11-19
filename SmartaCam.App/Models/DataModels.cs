using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartaCam
{
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
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
