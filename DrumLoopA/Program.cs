using System;
using System.IO;
using ProceduralMusic;

namespace DrumLoopA
{
    class Program
    {
        static void Main(string[] args)
        {
            var bpm = 170.0;

            var sampleDir = Path.Combine("..", "..", "..", "Samples");
            var drumDir = Path.Combine(sampleDir, "DrumHits");
            var hiHatDir = Path.Combine(drumDir, "HiHats");
            var snareDir = Path.Combine(drumDir, "Snares");
            var bassDrumDir = Path.Combine(drumDir, "BassDrums");
            var synthDir = Path.Combine(sampleDir, "Synth");
            var bassDir = Path.Combine(sampleDir, "BassSounds");

            var bass = new SampleFile(Path.Combine(bassDir, "DJF_C_SEXUAL808_BASS.wav")).Scale(0.2);
            var pad = new SampleFile(Path.Combine(synthDir, "DJF_C_SUNRISE.wav")).Scale(0.1);
            
            var hiHat = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_HINGE.wav")).Scale(0.4);
            var crash = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_JINAL.wav")).Scale(0.1);
            var snare = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_HEAT_BEAST.wav")).Scale(0.27);
            var bassDrum = new SampleFile(Path.Combine(bassDrumDir, "DJF_BD_KNOCKERS.wav")).Scale(0.33);

            Func<Source, Source> fade = (x) => x
                .FadeIn(15.0 / bpm).PhaseShift(-60.0 / bpm * 12)
                .FadeOut(60.0 / bpm * 12).PhaseShift(60.0 / bpm * 12);

            var seq = new Sequence(bpm / 64.0) {
                { 0, new Sequence(bpm / 16.0) {
                    { 0, bass },
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
                    { 2, new Sequence(bpm) {
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
                    { 3, new Sequence(bpm) {
                        { 0,  hiHat.Repeat(bpm, 2, 5) },
                        { 1,  snare },
                        { 2,  bassDrum },
                        { 3,  bassDrum },
                        { 4,  snare },
                        { 7,  snare },
                        { 9,  snare },
                        { 10, crash.Echo(30.0 / bpm, 0.7) },
                        { 10, bassDrum },
                        { 12, hiHat.Repeat(bpm, 2, 2) },
                        { 14, snare }
                    } },
                }.Repeat(bpm, 64, 4) },
                { 0, fade(pad) },
                { 1, fade(pad.PitchShift(0.84089622337261497225281353842922)) },
                { 2, fade(pad) },
                { 3, fade(pad.PitchShift(1.0594623519771811343021158573439)) },
            };

            Sampler.PreviewPlayback(seq, 0.0, 15.0 / bpm * 272);
        }
    }
}
