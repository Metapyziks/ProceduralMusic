using System;

namespace ProceduralMusic
{
    public abstract class Source
    {
        public static long SampleOffset(double time, int freq)
        {
            return (long) (time * freq);
        }

        public abstract double Sample(double t);

        public virtual void Sample(double[] buffer, double start, double duration, int freq)
        {
            Sample(buffer, 0, start, duration, freq);
        }

        public virtual void Sample(double[] buffer, int offset, double start, double duration, int freq)
        {
            long samples = Math.Min(buffer.Length - offset, SampleOffset(duration, freq));
            for (long i = samples - 1; i >= 0; --i) {
                buffer[i + offset] = Sample(start + i / (double) freq);
            }
        }

        public Scale Scale(double scale)
        {
            return new Scale(this, scale);
        }

        public Repeat Repeat(double bpm, int beats, int repetitions)
        {
            return new Repeat(this, 15.0 * beats / bpm, repetitions);
        }

        public Repeat Repeat(double period, int repetitions)
        {
            return new Repeat(this, period, repetitions);
        }
    }
}
