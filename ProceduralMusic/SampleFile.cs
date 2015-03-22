using System;
using System.IO;

namespace ProceduralMusic
{
    public class SampleFile : Source
    {
        private static float BytesToSingle(byte firstByte, byte secondByte)
        {
            short s = (short) ((secondByte << 8) | firstByte);
            return s / 32768f;
        }

        private static float ReadSample(byte[] wav, int offset, int bps)
        {
            ulong val = 0;
            for (int i = 0; i < (bps >> 3); ++i) {
                val |= (ulong) wav[offset + i] << (8 * i);
            }

            if (val >> (bps - 1) != 0) {
                return -(((~val + 1) & (ulong) ((1 << bps) - 1)) / (float) (1 << (bps - 1)));
            } else {
                return val / (float) (1 << (bps - 1));
            }
        }

        private static void OpenWav(string filename, out int freq, out float[] buffer)
        {
            byte[] wav = File.ReadAllBytes(filename);

            // Determine if mono or stereo
            int channels = wav[22];     // Forget byte 23 as 99.999% of WAVs are 1 or 2 channels

            freq = BitConverter.ToInt32(wav, 24);

            int bps = BitConverter.ToInt16(wav, 34);

            // Get past all the other sub chunks to get to the data subchunk:
            int pos = 12;   // First Subchunk ID from 12 to 16

            // Keep iterating until we find the data chunk (i.e. 64 61 74 61 ...... (i.e. 100 97 116 97 in decimal))
            while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97)) {
                pos += 4;
                int chunkSize = wav[pos] + wav[pos + 1] * 256 + wav[pos + 2] * 65536 + wav[pos + 3] * 16777216;
                pos += 4 + chunkSize;
            }

            pos += 4;
            int size = BitConverter.ToInt32(wav, pos);
            pos += 4;
            
            // Pos is now positioned to start of actual sound data.
            int samples = (size - pos) / (bps >> 3);
            if (channels == 2) samples /= 2;

            // Allocate memory (right will be null if only mono sound)
            buffer = new float[samples];

            // Write to double array/s:
            int i = 0;
            while (pos < size && i < samples) {
                buffer[i] = ReadSample(wav, pos, bps);
                pos += (bps >> 3);
                if (channels == 2) {
                    buffer[i] = (buffer[i] + ReadSample(wav, pos, bps)) * 0.5f;
                    pos += (bps >> 3);
                }
                i++;
            }
        }

        private float[] _samples;

        private int _sampleCount;
        private int _freq;

        public double Duration
        {
            get { return _sampleCount / (double) _freq; }
        }

        public SampleFile(String path)
        {
            OpenWav(path, out _freq, out _samples);

            _sampleCount = _samples.Length;
        }


        public override float Sample(double t)
        {
            long i = SampleOffset(t, _freq);

            if (i < _sampleCount) {
                return _samples[i];
            } else {
                return 0.0f;
            }
        }
    }
}
