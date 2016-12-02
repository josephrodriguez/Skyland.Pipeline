#region using

using System;
using System.Reflection;
using NUnit.Framework;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class DefaultFixture
    {
        [Test]
        public void CreatePipelineWithInlineJobs()
        {
            var evenPipeline = new PipelineBuilder<string, bool>()
                .Register<string, string>(s => s.Trim())
                .Register<string, int>(int.Parse)
                .Register<int, bool>(i => i % 2 == 0)
                .Build();

            var result = evenPipeline.Execute(" 56  ");

            Assert.IsTrue(result);
        }

        [Test]
        public void HandleExceptionOnPipeline()
        {
            var handledException = false;

            var pipeline = new PipelineBuilder<int, int>()
                .Register<int, int>(s => { throw new NotImplementedException(); })
                .OnError((sender, exception) => handledException = true)
                .Build();

            var result = pipeline.Execute(0);

            Assert.IsTrue(handledException);
        }

        [Test]
        public void UnhandledException()
        {
            var pipeline = new PipelineBuilder<int, int>()
                .Register<int, int>(s => { throw new NotImplementedException(); })
                .Build();

            Assert.Throws<NotImplementedException>(() => pipeline.Execute(0));
        }
    }
}
