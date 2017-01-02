#region using

using System;
using NUnit.Framework;
using Skyland.Pipeline.Exceptions;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class ExceptionsFixture
    {
        [Test]
        public void RegisterJobException()
        {
            var builder = new Pipeline<Type, string>.Builder()
                .Register<Type, string>(
                    stage =>
                        stage
                            .Job(type => type.FullName)
                            .Job(type => type.Name));

            Assert.Throws<PipelineException>(() => builder.Build());
        }

        [Test]
        public void RegisterFilterAfterJob()
        {
            var builder = new Pipeline<DateTime?, string>.Builder()
                .Register<DateTime?, string>(
                    stage =>
                        stage
                            .Job(date => date?.ToLongDateString())
                            .Filter(date => date != null));

            Assert.Throws<PipelineException>(() => builder.Build());
        }

        [Test]
        public void RegisterHandlerBeforeJob()
        {
            var builder = new Pipeline<string, bool>.Builder()
                .Register<string, bool>(
                    stage =>
                        stage
                            .Handler(Console.WriteLine));

            Assert.Throws<PipelineException>(() => builder.Build());
        }

        [Test]
        public void RegisterStageWithoutJob()
        {
            Action function =
                () =>
                {
                    new Pipeline<Type, bool>.Builder()
                        .Register<Type, bool>(
                            stage =>
                                stage
                                    .Handler(Console.WriteLine))
                        .Build();
                };

            Assert.Throws<PipelineException>(() => function());
        }

        [Test]
        public void MissmatchInputType()
        {
            Func<Pipeline<string, int>.Builder> func =
                () =>
                    new Pipeline<string, int>.Builder()
                        .Register<bool, int>(
                            stage =>
                                stage
                                    .Job(b => b ? 1 : 0));

            Assert.Throws<PipelineException>(() => func.Invoke());
        }

        [Test]
        public void MissmatchOutputType()
        {
            var builder = 
                new Pipeline<string, bool>.Builder()
                    .Register<string, int>(
                        stage =>
                            stage
                                .Job(int.Parse));

            Assert.Throws<PipelineException>(() => builder.Build());
        }
    }
}
