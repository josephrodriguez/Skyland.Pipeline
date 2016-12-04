#region using

using System;
using Skyland.Pipeline.Filters;

#endregion

namespace Skyland.Pipeline.Impl.Filters
{
    internal class InlinePipelineFilter<T> : IPipelineFilter<T>
    {
        private readonly Func<T, bool> _function;

        public InlinePipelineFilter(Func<T, bool> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");

            _function = function;
        }

        public bool Filter(T input)
        {
            return _function.Invoke(input);
        }
    }
}
