using System;
using System.Linq;
using System.Threading;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace ProceduralMusic
{
    public static class Sampler
    {
        public static short[] SampleInt16Buffer(Source src, double start, double duration, int sampleRate = 44100)
        {
            double[] doubleData = new double[(int) (duration * sampleRate)];

            src.Sample(doubleData, start, duration, sampleRate);

            short[] shortData = new short[doubleData.Length];
            for (int i = 0; i < doubleData.Length; ++i) {
                shortData[i] = (short) (doubleData[i] * (short.MaxValue));
            }

            return shortData;
        }

        public static void PreviewPlayback(Source src, double start, double duration, int repetitions = 1, int sampleRate = 44100)
        {
            using (AudioContext context = new AudioContext()) {
                int buffer = AL.GenBuffer();
                int source = AL.GenSource();
                int state;

                short[] shortData = Sampler.SampleInt16Buffer(src, start, duration, sampleRate);
                AL.BufferData(buffer, ALFormat.Mono16, shortData, shortData.Length * 2, sampleRate);
                AL.SourceQueueBuffers(source, repetitions, Enumerable.Repeat(buffer, repetitions).ToArray());

                var error = AL.GetError();
                if (error != ALError.NoError) {
                    throw new Exception(AL.GetErrorString(error));
                }

                AL.SourcePlay(source);
                do {
                    Thread.Sleep(1);
                    AL.GetSource(source, ALGetSourcei.SourceState, out state);
                }
                while ((ALSourceState) state == ALSourceState.Playing);

                AL.SourceStop(source);

                AL.DeleteSource(source);
                AL.DeleteBuffer(buffer);
            }
        }
    }
}
