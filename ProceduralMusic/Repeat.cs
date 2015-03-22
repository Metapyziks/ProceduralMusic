using System;

namespace ProceduralMusic
{
    public class Repeat : Source
    {
        private Source _src;
        private double _period;
        private int _repetitions;

        public Repeat(Source src, double period, int repetitions)
        {
            _src = src;
            _period = period;
            _repetitions = repetitions;
        }

        public override float Sample(double t)
        {
            var tr = t - Math.Floor(t / _period) * _period;
            var tc = Math.Floor(t / _period);

            if (_repetitions != 0 && tc > _repetitions) return 0.0f;

            return (_repetitions == 0 || tc < _repetitions ? _src.Sample(tr) : 0.0f)
                + (t >= _period ? _src.Sample(tr + _period) : 0.0f);
        }
    }
}
