using System.Collections.Generic;
using System.Linq;

namespace ProceduralMusic
{
    public class SequenceElement
    {
        public Source Source { get; private set; }
        public double Delay { get; private set; }

        public SequenceElement(Source source, double delay)
        {
            Source = source;
            Delay = delay;
        }
    }

    public class Sequence : Source, IEnumerable<SequenceElement>
    {
        private double _beatPeriod;
        private List<SequenceElement> _elems;

        public Sequence(double bpm)
        {
            _beatPeriod = 15.0 / bpm;
            _elems = new List<SequenceElement>();
        }

        public void Add(double delay, Source source)
        {
            _elems.Add(new SequenceElement(source, delay));
        }

        public void Add(int beat, Source source)
        {
            _elems.Add(new SequenceElement(source, _beatPeriod * beat));
        }

        public override float Sample(double t)
        {
            return _elems
                .Where(x => x.Delay < t)
                .Sum(x => x.Source.Sample(t - x.Delay));
        }

        public IEnumerator<SequenceElement> GetEnumerator()
        {
            return _elems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _elems.GetEnumerator();
        }
    }
}
