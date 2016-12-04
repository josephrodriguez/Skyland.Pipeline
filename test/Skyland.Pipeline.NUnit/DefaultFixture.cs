#region using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                .Register(
                    Component
                        .ForJob<string, string>(s => s.Trim())
                        .WithFilter(string.IsNullOrEmpty)
                        .WithFilter(s => s.Length == 1),
                    Component
                        .ForJob<string, int>(int.Parse)
                        .WithHandler(Console.WriteLine)
                        .WithHandler(i => Trace.WriteLine(i)),
                    Component
                        .ForJob<int, bool>(i => i % 2 == 0)
                        )
                .Build();

            var result = evenPipeline.Execute(" 56  ");

            Assert.IsTrue(result);
        }

        [Test]
        public void HandleExceptionOnPipeline()
        {
            var handledException = false;

            var pipeline = new PipelineBuilder<int, int>()
                .Register(Component.ForJob<int, int>(s => { throw new NotImplementedException(); }))
                .OnError((sender, exception) => handledException = true)
                .Build();

            pipeline.Execute(0);

            Assert.IsTrue(handledException);
        }

        [Test]
        public void UnhandledException()
        {
            var pipeline = new PipelineBuilder<int, int>()
                .Register(Component.ForJob<int, int>(s => { throw new NotImplementedException(); }))
                .Build();

            Assert.Throws<NotImplementedException>(() => pipeline.Execute(0));
        }
    }
}
