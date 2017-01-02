#region using

using System;
using NUnit.Framework;
using Skyland.Pipeline.Components;
using Skyland.Pipeline.NUnit.Classes;

#endregion

namespace Skyland.Pipeline.NUnit
{
    [TestFixture]
    public class FiltersFixture
    {
        [Test]
        public void RegisterFilterComponentWithArgumentNullException()
        {
            var builder = new Pipeline<MockClassA, int>.Builder()
                .Register<MockClassA, int>(
                    stage =>
                        stage
                            .Filter((IFilterComponent<MockClassA>)null)
                            .Job(mock => mock.Field1));

            Assert.Throws<ArgumentNullException>(() => builder.Build());
        }

        [Test]
        public void RegisterFilterFunctionWithArgumentNullException()
        {
            var builder = new Pipeline<MockClassA, int>.Builder()
                .Register<MockClassA, int>(
                    stage =>
                        stage
                            .Filter((Func<MockClassA, bool>)null)
                            .Job(mock => mock.Field1));

            Assert.Throws<ArgumentNullException>(() => builder.Build());
        }

        [Test]
        public void RejectedOutputByFilter()
        {
            var pipeline = new Pipeline<MockClassB, int>.Builder()
                .Register<MockClassA, int>(
                    stage =>
                        stage
                            .Filter(mock => mock.Field1 > 0)
                            .Job(mock => mock.Field1))
                .Build();

            var output = pipeline.Execute(new MockClassB());

            Assert.IsTrue(output.IsRejected);
        }
    }
}
