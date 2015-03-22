using System;

namespace ProceduralMusic
{
    public class Echo : Source
    {
        private Source _src;
        private double _delay;
        private float _decay;

        public Echo(Source src, double delay, float decay)
        {
            _src = src;
            _delay = delay;
            _decay = decay;
        }

        public override float Sample(double t)
        {
            var reps = Math.Floor(t / _delay);
            var vol = 1.0f;
            var sum = 0.0f;

            for (int i = 0; i <= reps; ++i) {
                sum += _src.Sample(t - i * _delay) * vol;
                vol *= _decay;
                if (vol < 0.01f) break;
            }

            return sum;
        }
    }
}
