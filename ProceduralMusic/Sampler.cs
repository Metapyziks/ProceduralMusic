using System;
using System.Diagnostics;
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
                shortData[i] = (short) (Math.Min(1.0, Math.Max(-1.0, doubleData[i])) * (short.MaxValue));
            }

            return shortData;
        }

        public static void PreviewPlayback(Source src, double start, double duration, int repetitions = 1, int sampleRate = 44100)
        {
            using (AudioContext context = new AudioContext()) {
                int samplesPerBuffer = sampleRate / 2;
                int sampleCount = (int) (duration * sampleRate);
                int bufferCount = (int) Math.Ceiling((double) sampleCount / samplesPerBuffer);

                int[] buffers = AL.GenBuffers(bufferCount);
                int source = AL.GenSource();

                var bufferDuration = (double) samplesPerBuffer / sampleRate;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[{0}]", String.Join("", Enumerable.Repeat(' ', Console.WindowWidth - 2)));
                Console.CursorTop = 0;
                Console.ResetColor();

                var timer = new Stopwatch();

                int bufferX = 1;
                int playbackX = 1;
                for (int i = 0; i < bufferCount; ++i) {
                    double offset = bufferDuration * i;
                    short[] shortData = Sampler.SampleInt16Buffer(src, start + offset, Math.Min(bufferDuration, duration - offset), sampleRate);
                    AL.BufferData(buffers[i], ALFormat.Mono16, shortData, shortData.Length * 2, sampleRate);
                    AL.SourceQueueBuffers(source, 1, new int[] { buffers[i] });

                    int nextX = 1 + ((Console.WindowWidth - 2) * (i + 1)) / bufferCount;
                    if (nextX > bufferX) {
                        Console.CursorLeft = bufferX;
                        Console.Write(String.Join("", Enumerable.Repeat('-', nextX - bufferX)));
                        bufferX = nextX;
                    }

                    nextX = 1 + (int) Math.Round(((Console.WindowWidth - 2) * timer.Elapsed.TotalSeconds) / duration);
                    if (nextX > playbackX) {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = playbackX;
                        Console.Write(String.Join("", Enumerable.Repeat('=', nextX - playbackX)));
                        Console.ResetColor();
                        playbackX = nextX;
                    }

                    if (i == 0) {
                        AL.SourcePlay(source);
                        timer.Start();
                    }
                }

                int state;
                do {
                    int nextX = 1 + (int) Math.Round(((Console.WindowWidth - 2) * timer.Elapsed.TotalSeconds) / duration);
                    if (nextX > playbackX) {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = playbackX;
                        Console.Write(String.Join("", Enumerable.Repeat('=', nextX - playbackX)));
                        Console.ResetColor();
                        playbackX = nextX;
                    }

                    Thread.Sleep(1);
                    AL.GetSource(source, ALGetSourcei.SourceState, out state);
                }
                while ((ALSourceState) state == ALSourceState.Playing);

                AL.SourceStop(source);

                AL.DeleteSource(source);
                AL.DeleteBuffers(buffers);
            }
        }
    }
}
