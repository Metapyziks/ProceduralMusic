using System;

namespace ProceduralMusic
{
    public abstract class Source
    {
        public static long SampleCount(double duration, double freq)
        {
            return (long) Math.Ceiling(duration / freq);
        }

        public abstract float Sample(double t);
        public virtual void Sample(float[] buffer, double start, double duration, double freq)
        {
            Sample(buffer, 0, start, duration, freq);
        }

        public virtual void Sample(float[] buffer, int offset, double start, double duration, double freq)
        {
            long samples = Math.Min(buffer.Length - offset, SampleCount(duration, freq));
            for (long i = samples - 1; i >= 0; --i) {
                buffer[i + offset] = Sample(start + i * freq);
            }
        }
    }
}
