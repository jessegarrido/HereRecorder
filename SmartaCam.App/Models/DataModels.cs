using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartaCam
{
    public class Take
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RunLengthInSeconds { get; set; }
        public decimal OriginalPeakVolume { get; set; }
        public float ChannelOneInputPeak { get; set; }
        public float ChannelTwoInputPeak { get; set; }
        public string FileName { get; set; } = string.Empty;
        public bool Normalized { get; set; }
        public bool IsMono { get; set; }
        public bool WasConvertedToMp3 { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public bool WasUpLoaded { get; set; }
        public DateTime Created { get; set; }
        public TimeSpan Duration { get; set; }
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
