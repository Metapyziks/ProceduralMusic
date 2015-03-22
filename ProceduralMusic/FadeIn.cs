
namespace ProceduralMusic
{
    public class FadeIn : Source
    {
        private Source _src;
        private double _duration;

        public FadeIn(Source source, double duration)
        {
            _src = source;
            _duration = duration;
        }

        public override float Sample(double t)
        {
            if (t < 0.0) return 0.0f;
            if (t >= _duration) return _src.Sample(t);
            return _src.Sample(t) * (float) (t / _duration);
        }
    }
}
