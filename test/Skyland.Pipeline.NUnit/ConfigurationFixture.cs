using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.NUnit.Classes;

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class ConfigurationFixture
    {
        [Test]
        public void EmptyPipelineConfigurationFixture()
        {
            var builder = new Pipeline<MockClassA, long>.Builder();

            Assert.Throws<PipelineException>(() => builder.Build());
        }

        [Test]
        public void NullStageComponentConfiguratorFixture()
        {
            Func<Pipeline<MockClassA, long>.Builder> func =
                () => 
                    new Pipeline<MockClassA, long>.Builder()
                        .Register<MockClassA, long>(null);

            Assert.Throws<ArgumentNullException>(() => func());
        }

        [Test]
        public void NullPipelineErrorHandlerFixture()
        {
            Func<IPipeline<int, MockClassA>> func =
                () =>
                    new Pipeline<int, MockClassA>.Builder()
                        .Register<int, MockClassA>(
                            stage =>
                                stage.Job(i => new MockClassB() {Field1 = i}))
                        .OnError(null)
                        .Build();

            Assert.Throws<ArgumentNullException>(() => func());
        }

        [Test]
        public void MissmatchStageTypeFixture()
        {
            Func<IPipeline<string, MockClassA>> func =
                () =>
                    new Pipeline<string, MockClassA>.Builder()
                        .Register<string, int>(
                            stage =>
                                stage.Job(int.Parse))
                        .Register<long, MockClassA>(
                            stage =>
                                stage.Job(l => new MockClassA()))
                        .OnError(null)
                        .Build();

            Assert.Throws<PipelineException>(() => func());
        }
    }
}
