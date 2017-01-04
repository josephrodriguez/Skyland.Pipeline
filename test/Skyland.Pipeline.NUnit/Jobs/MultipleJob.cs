#region using



#endregion

using System;
using Skyland.Pipeline.Components;
using Skyland.Pipeline.Components.Job;

namespace Skyland.Pipeline.NUnit.Jobs
{
    internal class MultipleJob : IJobComponent<string, string>, IJobComponent<string, int>
    {
        public string Execute(string input)
        {
            return input.Trim();
        }

        int IJobComponent<string, int>.Execute(string input)
        {
            return input.Length;
        }
    }
}
