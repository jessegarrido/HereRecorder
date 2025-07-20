using PortAudioSharp;

namespace HERE
{
	public class Config
	{
		//load from App.config
		public static string SSID { get; set; } = string.Empty;

		public static string SSIDpw { get; set; } = string.Empty;

		public static string DbCode { get; set; } = string.Empty;

		public static string DbApiKey { get; set; } = string.Empty;

		public static string DbApiSecret { get; set; } = string.Empty;

		public static int SampleRate { get; set; }
		public static int SelectedAudioDevice { get; set; } = 0;
		public static bool Normalize { get; set; } = false;
		public static bool NormalizeSplitChannels { get; set; } = false;
		public static bool PushToCloud { get; set; } = false;
		public static bool CopyToUsb { get; set; } = false;
		public static int Mp3BitRate { get; set; } = 192;
		public static string LocalRecordingsFolder { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "HERE", "Recordings");
		public static string RemovableDrivePath { get; set; } = string.Empty;
		public static List<string>? RemovableDrivePaths { get; set; } = new();
		public static string DropBoxCodeTxt { get; set; } = string.Empty;
		//public static string MixMode { get; set; } = "blend";
		public static bool DownmixToMono { get; set; } = false;


		public static int GreenLED { get; set; } = 2; //orange - pin 3
		public static int YellowLED { get; set; } = 3; // green - pin 5 
		public static int RedLED { get; set; } = 4; // purple - pin 7
		public static int PlayButton { get; set; } = 17; // grey - pin 8
		public static int RecordButton { get; set; } = 15; // black - pin 10      
		public static int StopButton { get; set; } = 22;// = brown - pin 11
		public static int BackButton { get; set; } = 18; // red - pin 12
		public static int ForwardButton { get; set; } = 27; // orange - pin 13
		public static int FootPedal { get; set; } = 14; // Green, Pin 15
		public static List<string> PingIPList { get; set; } = ["1.1.1.1", "8.8.8.8", "208.67.222.222"];

		public static StreamParameters SetAudioParameters()
		{
			StreamParameters param = new StreamParameters();
			DeviceInfo info = PortAudio.GetDeviceInfo(Config.SelectedAudioDevice);
			param.device = Config.SelectedAudioDevice;
			param.channelCount = 2;
			param.sampleFormat = SampleFormat.Float32;
			//param.suggestedLatency = info.defaultLowInputLatency;
			param.suggestedLatency = .9;
			param.hostApiSpecificStreamInfo = nint.Zero;
			return param;
		}
	}
}
