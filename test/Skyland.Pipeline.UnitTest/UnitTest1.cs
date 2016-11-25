using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Skyland.Pipeline.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pipeline = new PipelineBuilder<string, int>()
                .Register(new Job1())
                .Register(new Job2())
                .Build();
        }
    }

    public class Job1 : IPipelineJob<string, string>
    {
        public string Process(string input)
        {
            return input.Trim();
        }
    }

    public class Job2 : IPipelineJob<string, int>
    {
        public int Process(string input)
        {
            return int.Parse(input);
        }
    }
}
