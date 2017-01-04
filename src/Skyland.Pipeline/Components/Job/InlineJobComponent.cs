#region using

using System;

#endregion

namespace Skyland.Pipeline.Components.Job
{
    internal class InlineJobComponent<TInput, TOutput> : IJobComponent<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _function;

        public InlineJobComponent(Func<TInput, TOutput> function)
        {
            _function = function;
        }  

        public TOutput Execute(TInput input)
        {
            return _function(input);
        }
    }
}
