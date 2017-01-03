#region using

using System;
using NUnit.Framework;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.NUnit.Classes;
using Skyland.Pipeline.NUnit.Jobs;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class JobsFixture
    {
        [Test]
        public void CreateInlineJobs()
        {
            var evenPipeline = new Pipeline<string, bool>.Builder()
                .Register<string, string>(
                    stage =>
                        stage
                            .Filter(s => !string.IsNullOrEmpty(s))
                            .Filter(s => s.Length == 5)
                            .Job(s => s.Trim()))
                .Register<string, int>(
                    stage =>
                        stage
                            .Job(int.Parse)
                            .Handler(Console.WriteLine))
                .Register<int, bool>(
                    stage =>
                        stage.Job(i => i%2 == 0))
                .Build();
                   
            var response = evenPipeline.Execute(" 56  ");

            Assert.IsTrue(response.Result);
        }

        [Test]
        public void GenericWithInlineJob()
        {
            var pipeline = new Pipeline<string, bool>.Builder()
                .Register<string, int>(
                    stage =>
                        stage.Job<MultipleJob>())
                .Register<int, bool>(
                    stage =>
                        stage.Job(i => i % 2 == 0))
                .Build();

            var response = pipeline.Execute("ABCD");

            Assert.IsTrue(response.Result);
            Assert.IsTrue(response.IsCompleted);
        }

        [Test]
        public void RegisterSameJobClass()
        {
            var pipeline = new Pipeline<string, int>.Builder()
                .Register<string, string>(
                    stage =>
                        stage.Job<MultipleJob>())
                .Register<string, int>(
                    stage =>
                        stage.Job(new MultipleJob()))
                .Build();

            var response = pipeline.Execute("   ABCD");

            Assert.IsTrue(response.Result == 4);
            Assert.IsTrue(response.IsCompleted);
        }

        [Test]
        public void HandleExceptionOnPipeline()
        {
            var handledException = false;

            var pipeline = new Pipeline<int, int>.Builder()
                .Register<int, int>(
                    stage => 
                        stage.Job(i => i/0))
                .OnError((sender, exception) => handledException = true)
                .Build();

            pipeline.Execute(0);

            Assert.IsTrue(handledException);
        }

        [Test]
        public void UnhandledException()
        {
            var pipeline = new Pipeline<int, int>.Builder()
                .Register<int, int>(
                    stage =>
                        stage.Job(i => { throw new NotImplementedException(); }))
                .Build();

            Assert.Throws<NotImplementedException>(() => pipeline.Execute(0));
        }

        [Test]
        public void NullJobComponentFunctionFixture()
        {

            var builder = new Pipeline<int, MockClassA>.Builder()
                .Register<int, MockClassA>(
                    stage =>
                        stage
                            .Job((Func<int, MockClassA>) null)
                            .Handler<MockHandler2>());

            Assert.Throws<ArgumentNullException>(() => builder.Build());
        }

        [Test]
        public void BuilderWithoutRegisteredJobFixture()
        {
            var builder = new Pipeline<MockClassA, string>.Builder()
                .Register<MockClassA, string>(
                    stage =>
                        stage.Filter(mock => mock.Field1 >= 0));

            Assert.Throws<PipelineException>(() => builder.Build());
        }
    }
}
