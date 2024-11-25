//using NAudio;
//namespace SmartaCam
//{
//    public class UtilClass
//    {

//        //* A minimalistic C# file writer that writes IEEE float32 wave files
//        //*/
//        public class Float32WavWriter : IDisposable
//        {
//            private const int bytesPerSample = 4;

//            private readonly BinaryWriter writer;
//            private readonly int channels;

//            private int writtenSamples = 0;

//            public Float32WavWriter(string filePath, int sample_rate, int channels)
//            {

//                // writer = new BinaryWriter(File.OpenWrite(filePath));//, FileMode.Open, FileAccess.Write, FileShare.Read));

//                writer = new BinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read));

//                this.channels = channels;
//                {
//                    // Write file header. Read This: http://soundfile.sapp.org/doc/WaveFormat/ / http://www-mmsp.ece.mcgill.ca/Documents/AudioFormats/WAVE/WAVE.html
//                    writer.Write(new char[] { 'R', 'I', 'F', 'F' });
//                    writer.Write(0); // total chunk size (4+chunks) (will fill in later)
//                    writer.Write(new char[] { 'W', 'A', 'V', 'E' });

//                    writer.Write(new char[] { 'f', 'm', 't', ' ' }); // "fmt " chunk (Google: WAVEFORMATEX structure)
//                    writer.Write(18); // PCM=16 // format-chunk size (in bytes): 16, 18 or 40
//                    writer.Write((ushort)3); // wFormatTag (PCM = 1, IEEE float = 3)
//                    writer.Write((ushort)channels); // wChannels
//                    writer.Write(sample_rate); // dwSamplesPerSec (blocks per s)
//                    writer.Write(sample_rate * channels * bytesPerSample); // dwAvgBytesPerSec
//                    writer.Write((ushort)(channels * bytesPerSample)); // wBlockAlign (blocksize)
//                    writer.Write((ushort)(bytesPerSample * 8)); // wBitsPerSample

//                    writer.Write((ushort)0); // cbSize = size of extension (not needed for PCM)
//                    writer.Write(new char[] { 'f', 'a', 'c', 't' }); // "fact" chunk (not needed for PCM)
//                    writer.Write(4); // fact-chunk size (min 4)  (not needed for PCM)
//                    writer.Write(0); // dwSampleLength = Number of blocks (samples "per channel") in the file (will fill in later)  (not needed for PCM)

//                    writer.Write(new char[] { 'd', 'a', 't', 'a' }); // "data" chunk
//                    writer.Write(0); // data-chunk size (will fill in later)
//                }
//            }


//            public void WriteSample(float sample)
//            {
//                writer.Write(sample);
//                ++writtenSamples;
//            }

//            public void WriteSamples(float[] samples)
//            {
//                for (int i = 0; i < samples.Length; i++)
//                {
//                    writer.Write(samples[i]);
//                }
//                writtenSamples += samples.Length;
//            }

//            public void Dispose()
//            {
//                writer.Seek(4, SeekOrigin.Begin); // seek to overall size (4 = RIFF)
//                writer.Write(50 + writtenSamples * bytesPerSample * channels); // 50=36+2+4+4+4, 36 = 4 (WAVE) + 4 (fmt ) + 4 (fmt-size) + 16 (fmt-chunk) + 4 (data) + 4 (data-size)
//                writer.Seek(46, SeekOrigin.Begin); // seek to data size position 46 = 4 (RIFF) + 4 (chsize) + 4 (WAVE) + 4 (fmt ) + 4 (fmt-size) + 18 (fmt-chunk) + 4 (fact) + 4 (fact-size)
//                writer.Write(writtenSamples);
//                writer.Seek(54, SeekOrigin.Begin); // seek to data size position 54=46+4+4 or 40+2+4+4+4, 40 = 4 (RIFF) + 4 (chsize) + 4 (WAVE) + 4 (fmt ) + 4 (fmt-size) + 16 (fmt-chunk) + 4 (data)
//                writer.Write(writtenSamples * bytesPerSample * channels);

//                writer.Dispose();
//            }
//        }
//    }
//}


