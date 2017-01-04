#region using

using System;

#endregion

namespace Skyland.Pipeline.Components.Filters
{
    internal class InlineFilterComponent<T> : IFilterComponent<T>
    {
        private readonly Func<T, bool> _function;

        public InlineFilterComponent(Func<T, bool> function)
        {
            _function = function;
        }

        public bool Execute(T input)
        {
            return _function.Invoke(input);
        }
    }
}
