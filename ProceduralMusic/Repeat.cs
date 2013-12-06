using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override double Sample(double t)
        {
            var tr = t - Math.Floor(t / _period) * _period;
            var tc = Math.Floor(t / _period);

            if (tc > _repetitions) return 0.0;
            
            return (tc < _repetitions ? _src.Sample(tr) : 0.0)
                + (t >= _period ? _src.Sample(tr + _period) : 0.0);
        }
    }
}
