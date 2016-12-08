#region using

using NUnit.Framework;
using Skyland.Pipeline.NUnit.Jobs;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class PipelineJobFixture
    {
        [Test]
        public void SupportMultipleJobs()
        {
            var pipeline = new PipelineBuilder<string, bool>()
                .Register(
                    new StageComponent<string, int>(new MultipleJob()))
                .Register(
                    new StageComponent<int, bool>(i => i % 2 == 0))
                .Build();

            var output = pipeline.Execute("ABCD");

            Assert.IsTrue(output.Result);
        }

        [Test]
        public void RegisterSameMultipleJobs()
        {
            var pipeline = new PipelineBuilder<string, int>()
                .Register(
                    new StageComponent<string, string>(new MultipleJob()))
                .Register(
                    new StageComponent<string, int>(new MultipleJob()))
                .Build();

            var output = pipeline.Execute("   ABCD");

            Assert.IsTrue(output.Result == 4);
        }
    }
}
