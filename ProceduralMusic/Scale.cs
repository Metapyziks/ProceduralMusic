
namespace ProceduralMusic
{
    public class Scale : Source
    {
        private Source _src;
        private float _scale;

        public Scale(Source source, float scale)
        {
            _src = source;
            _scale = scale;
        }

        public override float Sample(double t)
        {
            return _src.Sample(t) * _scale;
        }
    }
}
