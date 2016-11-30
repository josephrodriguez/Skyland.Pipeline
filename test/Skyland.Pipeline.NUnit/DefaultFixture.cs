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
        public void CreateInlineJob()
        {
            var pipeline = new PipelineBuilder<string, int>()
                .Register<string, int>(int.Parse)
                .Register<int, int>(i => i%2)
                .OnError((sender, exception) => Console.WriteLine(exception))
                .Build();

            var result = pipeline.Execute(" 123");
        }
        
    }
}
