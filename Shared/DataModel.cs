using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
	public class DataModel
	{
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
	}
}
