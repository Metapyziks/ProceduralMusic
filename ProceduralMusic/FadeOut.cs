
namespace ProceduralMusic
{
    public class FadeOut : Source
    {
        private Source _src;
        private double _duration;

        public FadeOut(Source source, double duration)
        {
            _src = source;
            _duration = duration;
        }

        public override double Sample(double t)
        {
            if (t < 0.0) return _src.Sample(t);
            if (t >= _duration) return 0.0;
            return _src.Sample(t) * (1.0 - (t / _duration));
        }
    }
}
