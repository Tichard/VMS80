using System.Diagnostics;

namespace VMS80
{
    public class AudioReader
    {
        public bool read_wav_from_file(string filename, out float[] a_data, out int a_nb_samples, out int a_nb_channels)
        {
            a_nb_samples = 0;
            a_nb_channels = 0;
            a_data = [];

            try
            {
                using (FileStream fs = File.OpenRead(filename))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();

                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    int fmtCode = reader.ReadInt16();
                    int channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    if (fmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(fmtExtraSize);
                    }

                    // chunk 2
                    int dataID = reader.ReadInt32();
                    int bytes = reader.ReadInt32();

                    // DATA
                    byte[] byteArray = reader.ReadBytes(bytes);

                    int bytesForSamp = bitDepth / 8;
                    int nValues = bytes / bytesForSamp;

                    a_nb_channels = channels;
                    a_nb_samples = nValues / channels;
                    switch (bitDepth)
                    {
                        case 32:
                            Int32[] data32 = new Int32[nValues];
                            Buffer.BlockCopy(byteArray, 0, data32, 0, bytes);
                            a_data = Array.ConvertAll(data32, e => e / (float)(Int32.MaxValue));
                            return true;
                        case 16:
                            Int16[] data16 = new Int16[nValues];
                            Buffer.BlockCopy(byteArray, 0, data16, 0, bytes);
                            a_data = Array.ConvertAll(data16, e => e / (float)(Int16.MaxValue));
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Could not read " + filename);
                return false;
            }
        }
    }
}