
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

        public override float Sample(double t)
        {
            if (t < 0.0) return _src.Sample(t);
            if (t >= _duration) return 0.0f;
            return _src.Sample(t) * (float) (1.0 - (t / _duration));
        }
    }
}
