using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralMusic
{
    public class PitchShift : Source
    {
        private Source _src;
        private double _scale;
        private double _duration;

        public PitchShift(Source source, double scale, double duration)
        {
            _src = source;
            _scale = scale;
            _duration = duration;
        }

        public override double Sample(double t)
        {
            if (t < 0.0) return _src.Sample(t);
            if (t >= _duration) return _src.Sample(t * _scale - 0.5 * _duration * (_scale - 1.0));
            return _src.Sample(t + 0.5 * t * t / _duration * (_scale - 1.0));
        }
    }
}
