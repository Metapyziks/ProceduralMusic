using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProceduralMusic;

namespace DrumLoopA
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleDir = Path.Combine("..", "..", "..", "Samples");
            var drumDir = Path.Combine(sampleDir, "DrumHits");
            var hiHatDir = Path.Combine(drumDir, "HiHats");
            var snareDir = Path.Combine(drumDir, "Snares");
            var bassDrumDir = Path.Combine(drumDir, "BassDrums");

            var hiHat = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_HINGE.wav")).Scale(0.4);
            var crash = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_JINAL.wav")).Scale(0.33);
            var snare = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_HEAT_BEAST.wav")).Scale(0.27);
            var bassDrum = new SampleFile(Path.Combine(bassDrumDir, "DJF_BD_TINY.wav")).Scale(0.33);

            var bpm = 174.0;

            var seq = new Sequence(bpm) {
                { new Sequence(bpm) {
                    { hiHat.Repeat(bpm, 2, 8), 0 },
                    { bassDrum, 0 },
                    { bassDrum, 2 },
                    { snare, 4 },
                    { snare, 7 },
                    { snare, 9 },
                    { bassDrum, 10 },
                    { bassDrum, 11 },
                    { snare, 12 },
                    { snare, 15 }
                }.Repeat(174.0, 16, 2), 0 },
                { new Sequence(bpm) {
                    { hiHat.Repeat(bpm, 2, 5), 0 },
                    { bassDrum, 0 },
                    { bassDrum, 2 },
                    { snare, 4 },
                    { snare, 7 },
                    { snare, 9 },
                    { hiHat.Scale(2.5), 10 },
                    { bassDrum, 10 },
                    { hiHat.Repeat(bpm, 2, 2), 12 },
                    { snare, 14 }
                }, 32 },
                { new Sequence(bpm) {
                    { hiHat.Repeat(bpm, 2, 5), 0 },
                    { snare, 1 },
                    { bassDrum, 2 },
                    { bassDrum, 3 },
                    { snare, 4 },
                    { snare, 7 },
                    { snare, 9 },
                    { crash, 10 },
                    { bassDrum, 10 },
                    { hiHat.Repeat(bpm, 2, 2), 12 },
                    { snare, 14 }
                }, 48 }
            }.Repeat(bpm, 64, 2);

            Sampler.PreviewPlayback(seq, 0.0, 12.0);
        }
    }
}
