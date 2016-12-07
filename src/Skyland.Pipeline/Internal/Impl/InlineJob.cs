#region using

using System;

#endregion

namespace Skyland.Pipeline.Impl
{
    internal class InlineJob<TInput, TOutput> : IPipelineJob<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _function;

        public InlineJob(Func<TInput, TOutput> function)
        {
            if(function == null)
                throw new ArgumentNullException();

            _function = function;
        }  

        public TOutput Process(TInput input)
        {
            return _function(input);
        }
    }
}
