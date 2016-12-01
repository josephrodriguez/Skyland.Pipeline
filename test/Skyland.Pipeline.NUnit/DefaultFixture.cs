#region using

using System;
using NUnit.Framework;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class DefaultFixture
    {
        [Test]
        public void CreateInlinePipelineJob()
        {
            var pipeline = new PipelineBuilder<string, int>()
                .Register<string, string>(s => s.Trim())
                .Register<string, int>(int.Parse)
                .Register<int, int>(i => i%2)
                .OnError((sender, exception) => Console.WriteLine(exception))
                .Build();

            var result = pipeline.Execute(" 123  ");

            Assert.AreEqual(result, 1);
        }

    }
}
