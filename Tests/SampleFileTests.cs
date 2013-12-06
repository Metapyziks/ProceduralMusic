using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProceduralMusic;

namespace Tests
{
    [TestClass]
    public class SampleFileTests
    {
        [TestMethod]
        public void WavFileTest()
        {
            var file = new SampleFile(Path.Combine(
                "..", "..", "..", "Samples", "DrumHits", "HiHats", "DJF_HH_NANG.wav"));

            Sampler.PreviewPlayback(file, 0.0, file.Duration, 10);
        }
    }
}
