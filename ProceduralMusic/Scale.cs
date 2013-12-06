
namespace ProceduralMusic
{
    public class Scale : Source
    {
        private Source _src;
        private double _scale;

        public Scale(Source source, double scale)
        {
            _src = source;
            _scale = scale;
        }

        public override double Sample(double t)
        {
            return _src.Sample(t) * _scale;
        }
    }
}
