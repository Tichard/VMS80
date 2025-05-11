using System.Diagnostics;

namespace VMS80
{
    public class AudioReader
    {
        static public bool read_wav_from_file(string filename, out float[] a_data, out int a_nb_samples, out int a_nb_channels, out int a_samplerate)
        {
            a_nb_samples = 0;
            a_nb_channels = 0;
            a_samplerate = 0;
            a_data = [];

            try
            {
                using FileStream fs = File.OpenRead(filename);
                BinaryReader reader = new(fs);

                // chunk 0
                int chunkID = reader.ReadInt16();
                while (chunkID != 18770) // Look for the first 16bits of chunkID
                {
                    chunkID = reader.ReadInt16();
                }
                chunkID = reader.ReadInt16(); // rest of chunkID (17990)

                int fileSize = reader.ReadInt32();
                int riffType = reader.ReadInt32();

                // chunk 1
                int fmtID = reader.ReadInt16();
                while (fmtID != 28006) // Look for the first 16bits of fmtID
                {
                    fmtID = reader.ReadInt16();
                }
                fmtID = reader.ReadInt16(); // rest of fmtID (8308)

                int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                // 16 bytes coming...
                int fmtCode = reader.ReadInt16();
                int channels = reader.ReadInt16();
                int sampleRate = reader.ReadInt32();
                int byteRate = reader.ReadInt32();
                int fmtBlockAlign = reader.ReadInt16();
                int bitDepth = reader.ReadInt16();

                // chunk 2
                int dataID = reader.ReadInt16();
                while (dataID != 24932) // Look for the first 16bits of dataID
                {
                    dataID = reader.ReadInt16();
                }
                dataID = reader.ReadInt16(); // rest of dataID (24948)

                int bytes = reader.ReadInt32();

                // DATA
                byte[] byteArray = reader.ReadBytes(bytes);

                int bytesForSamp = bitDepth / 8;
                int nValues = bytes / bytesForSamp;

                a_nb_channels = channels;
                a_nb_samples = nValues / channels;
                a_samplerate = sampleRate;
                switch (bitDepth)
                {
                    case 32:
                        Int32[] data32 = new Int32[nValues];
                        System.Buffer.BlockCopy(byteArray, 0, data32, 0, bytes);
                        a_data = Array.ConvertAll(data32, e => e / (float)(Int32.MaxValue));
                        return true;
                    case 16:
                        Int16[] data16 = new Int16[nValues];
                        System.Buffer.BlockCopy(byteArray, 0, data16, 0, bytes);
                        a_data = Array.ConvertAll(data16, e => e / (float)(Int16.MaxValue));
                        return true;
                    default:
                        return false;
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