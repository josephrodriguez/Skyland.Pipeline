#region using



#endregion

using System;

namespace Skyland.Pipeline.NUnit.Jobs
{
    internal class MultipleJob : IPipelineJob<string, string>, IPipelineJob<string, int>
    {
        public string Process(string input)
        {
            return input.Trim();
        }

        int IPipelineJob<string, int>.Process(string input)
        {
            return input.Length;
        }
    }
}
