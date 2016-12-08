#region using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class InlineJobFixture
    {
        [Test]
        public void CreatePipelineWithInlineJobs()
        {
            var evenPipeline = new PipelineBuilder<string, bool>()
                .Register(
                    new StageComponent<string,string>(s => s.Trim())
                        .WithFilter(s => !string.IsNullOrEmpty(s))
                        .WithFilter(s => s.Length == 5),
                    new StageComponent<string, int>(int.Parse)
                        .WithHandler(Console.WriteLine)
                        .WithHandler(i => Trace.WriteLine(i)),
                    new StageComponent<int, bool>(i => i % 2 == 0)
                        )
                .Build();

            var output = evenPipeline.Execute(" 56  ");

            Assert.IsTrue(output.Result);
        }

        [Test]
        public void HandleExceptionOnPipeline()
        {
            var handledException = false;

            var pipeline = new PipelineBuilder<int, int>()
                .Register(
                    new StageComponent<int, int>(s => { throw new NotImplementedException();})
                    )
                .OnError((sender, exception) => handledException = true)
                .Build();

            pipeline.Execute(0);

            Assert.IsTrue(handledException);
        }

        [Test]
        public void UnhandledException()
        {
            var pipeline = new PipelineBuilder<int, int>()
                .Register(new StageComponent<int, int>(s => { throw new NotImplementedException(); }))
                .Build();

            Assert.Throws<NotImplementedException>(() => pipeline.Execute(0));
        }
    }
}
