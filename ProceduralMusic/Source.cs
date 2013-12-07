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

        public FadeIn FadeIn(double duration)
        {
            return new FadeIn(this, duration);
        }

        public FadeOut FadeOut(double duration)
        {
            return new FadeOut(this, duration);
        }

        public Repeat Repeat(double bpm, int beats, int repetitions)
        {
            return new Repeat(this, 15.0 * beats / bpm, repetitions);
        }

        public Repeat Repeat(double period, int repetitions)
        {
            return new Repeat(this, period, repetitions);
        }

        public PitchShift PitchShift(double scale)
        {
            return new PitchShift(this, scale, 0.0);
        }

        public PitchShift PitchShift(double scale, double duration)
        {
            return new PitchShift(this, scale, duration);
        }

        public PhaseShift PhaseShift(double period)
        {
            return new PhaseShift(this, period);
        }

        public Echo Echo(double delay, double decay)
        {
            return new Echo(this, delay, decay);
        }
    }
}
