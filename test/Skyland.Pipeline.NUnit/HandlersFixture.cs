#region using

using System;
using NUnit.Framework;
using Skyland.Pipeline.Components;
using Skyland.Pipeline.NUnit.Classes;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class HandlersFixture
    {
        [Test]
        public void RegisterHandlerComponentWithArgumentNullException()
        {
            var builder = new Pipeline<MockClassA, int>.Builder()
                .Register<MockClassA, int>(
                    stage =>
                        stage
                            .Job(mock => mock.Field1)
                            .Handler((IHandlerComponent<int>) null));

            Assert.Throws<ArgumentNullException>(() => builder.Build());
        }

        [Test]
        public void RegisterHandlerFunctionWithArgumentNullException()
        {
            var builder = new Pipeline<MockClassA, int>.Builder()
                .Register<MockClassA, int>(
                    stage =>
                        stage
                            .Job(mock => mock.Field1)
                            .Handler((Action<int>) null));

            Assert.Throws<ArgumentNullException>(() => builder.Build());
        }

        [Test]
        public void RegisterGenericHandlerByTypeFixture()
        {
            var pipeline = new Pipeline<int, MockClassA>.Builder()
                .Register<int, MockClassA>(
                    stage =>
                        stage
                            .Job(i => new MockClassA() {Field1 = i})
                            .Handler<MockHandler2>())
                .Build();

            var output = pipeline.Execute(1);

            Assert.IsTrue(output.IsCompleted);
        }
    }
}
