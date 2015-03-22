using System;
using System.IO;
using ProceduralMusic;

namespace DrumLoopA
{
    class Program
    {
        static void Main(string[] args)
        {
            var bpm = 174.0;

            var sampleDir = Path.Combine("..", "..", "..", "Samples");
            var drumDir = Path.Combine(sampleDir, "DrumHits");
            var hiHatDir = Path.Combine(drumDir, "HiHats");
            var snareDir = Path.Combine(drumDir, "Snares");
            var bassDrumDir = Path.Combine(drumDir, "BassDrums");
            var synthDir = Path.Combine(sampleDir, "Synth");
            var sfxDir = Path.Combine(sampleDir, "SFX");
            var bassDir = Path.Combine(sampleDir, "BassSounds");

            var bass = new SampleFile(Path.Combine(bassDir, "DJF_C_SEXUAL808_BASS.wav")).Scale(0.2f);
            var pad = new SampleFile(Path.Combine(synthDir, "DJF_C_SUNRISE.wav")).Scale(0.1f)
                .PitchShift(0.25);
            var sfxA = new SampleFile(Path.Combine(sfxDir, "DJF_ALIEN_RECON.wav")).Scale(0.1f)
                .PitchShift(0.25);
            var sfxB = new SampleFile(Path.Combine(sfxDir, "DJF_AIRLOCK.wav")).Scale(0.2f)
                .PitchShift(0.25);
            
            var hiHat = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_HINGE.wav")).Scale(0.4f);
            var crash = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_JINAL.wav")).Scale(0.1f);
            var snare = new SampleFile(Path.Combine(hiHatDir, "DJF_HH_HEAT_BEAST.wav")).Scale(0.27f);
            var bassDrum = new SampleFile(Path.Combine(bassDrumDir, "DJF_BD_KNOCKERS.wav")).Scale(0.33f);

            Func<Source, Source> fade = (x) => x.PhaseShift(-60.0 / bpm * 12)
                .FadeOut(60.0 / bpm * 12).PhaseShift(60.0 / bpm * 12);

            var seq = new Sequence(bpm / 16.0) {
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
                        { 10, hiHat.Scale(2.5f) },
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
                        { 10, crash.Echo(30.0 / bpm, 0.7f).FadeIn(30.0 / bpm) },
                        { 10, bassDrum },
                        { 12, hiHat.Repeat(bpm, 2, 2) },
                        { 14, snare }
                    } },
                }.Repeat(bpm, 64, 4) },
                { 0, fade(pad) },
                { 1, sfxA },
                { 4, fade(pad.PitchShift(0.84089622337261497225281353842922)) },
                { 8, fade(pad) },
                { 11, sfxB.PhaseShift(-240 / bpm).FadeOut(-240 / bpm).PhaseShift(120 / bpm) },
                { 12, fade(pad.PitchShift(1.0594623519771811343021158573439)) },
            };

            Sampler.PreviewPlayback(seq, 0.0, 15.0 / bpm * 288);
        }
    }
}
