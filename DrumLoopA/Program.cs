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
            var bassDrum = new SampleFile(Path.Combine(bassDrumDir, "DJF_BD_KNOCKERS.wav")).Scale(0.33);

            var bpm = 170.0;

            var seq = new Sequence(bpm) {
                { 0, new Sequence(bpm) {
                    { 0,  hiHat.Repeat(bpm, 2, 8) },
                    { 0,  bassDrum },
                    { 2,  bassDrum },
                    { 4,  snare },
                    { 7,  snare },
                    { 9,  snare },
                    { 10, bassDrum },
                    { 11, bassDrum },
                    { 12, snare },
                    { 15, snare }
                }.Repeat(bpm, 16, 2) },
                { 32, new Sequence(bpm) {
                    { 0,  hiHat.Repeat(bpm, 2, 5) },
                    { 0,  bassDrum },
                    { 2,  bassDrum },
                    { 4,  snare },
                    { 7,  snare },
                    { 9,  snare },
                    { 10, hiHat.Scale(2.5) },
                    { 10, bassDrum },
                    { 12, hiHat.Repeat(bpm, 2, 2) },
                    { 14, snare }
                } },
                { 48, new Sequence(bpm) {
                    { 0,  hiHat.Repeat(bpm, 2, 5) },
                    { 1,  snare },
                    { 2,  bassDrum },
                    { 3,  bassDrum },
                    { 4,  snare },
                    { 7,  snare },
                    { 9,  snare },
                    { 10, crash },
                    { 10, bassDrum },
                    { 12, hiHat.Repeat(bpm, 2, 2) },
                    { 14, snare }
                } }
            }.Repeat(bpm, 64, 2);

            Sampler.PreviewPlayback(seq, 0.0, 11.5);
        }
    }
}
