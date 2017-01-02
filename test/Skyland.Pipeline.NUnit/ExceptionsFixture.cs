#region using

using System;
using System.Diagnostics;
using NUnit.Framework;
using Skyland.Pipeline.Components;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.NUnit.Classes;

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

        [Test]
        public void FilterContainerHandledException()
        {
            var pipeline = new Pipeline<DateTime, int>.Builder()
                .Register<DateTime, int>(
                    stage =>
                        stage
                            .Filter(datetime => { throw new NotImplementedException(); })
                            .Job(datetime => datetime.Millisecond))
                .OnError((sender, exception) => Trace.TraceError(exception.ToString()))
                .Build();

            var output = pipeline.Execute(DateTime.Now);

            Assert.True(output.IsFaulted);
        }

        [Test]
        public void FilterContainerUnhandledException()
        {
            var pipeline = new Pipeline<DateTime, int>.Builder()
                .Register<DateTime, int>(
                    stage =>
                        stage
                            .Filter(datetime => { throw new NotImplementedException(); })
                            .Job(datetime => datetime.Millisecond))
                .Build();

            Assert.Throws<NotImplementedException>(() => pipeline.Execute(DateTime.Now));
        }

        [Test]
        public void HandlerContainerHandledException()
        {
            var pipeline = new Pipeline<DateTime, int>.Builder()
                .Register<DateTime, int>(
                    stage =>
                        stage
                            .Job(datetime => datetime.Millisecond)
                            .Handler(i => { throw new NotImplementedException(); }))
                .OnError((sender, exception) => Trace.TraceError(exception.ToString()))
                .Build();

            var output = pipeline.Execute(DateTime.Now);

            Assert.True(output.IsFaulted);
        }

        [Test]
        public void HandlerContainerUnhandledException()
        {
            var pipeline = new Pipeline<DateTime, int>.Builder()
                .Register<DateTime, int>(
                    stage =>
                        stage
                            .Job(datetime => datetime.Millisecond)
                            .Handler(i => { throw new NotImplementedException(); }))
                .Build();

            Assert.Throws<NotImplementedException>(() => pipeline.Execute(DateTime.Now));
        }

        [Test]
        public void RejectedOutputByFilter()
        {
            var pipeline = new Pipeline<MockClassA, int>.Builder()
                .Register<MockClassA, int>(
                    stage =>
                        stage
                            .Filter<MockFilterA>()
                            .Job(mock => mock.Field1))
                .Build();

            var output = pipeline.Execute(new MockClassB() {Field1 = 10});

            Assert.IsTrue(output.IsCompleted);
            Assert.AreEqual(output.Result, 10);
        }

    }
}
