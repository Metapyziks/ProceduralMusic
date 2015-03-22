
namespace ProceduralMusic
{
    public class PhaseShift : Source
    {
        private Source _src;
        private double _period;

        public PhaseShift(Source src, double period)
        {
            _src = src;
            _period = period;
        }

        public override float Sample(double t)
        {
            return _src.Sample(t - _period);
        }
    }
}
