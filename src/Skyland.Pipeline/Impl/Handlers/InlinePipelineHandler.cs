#region using

using System;
using Skyland.Pipeline.Handler;
using Skyland.Pipeline.Handlers;

#endregion

namespace Skyland.Pipeline.Impl.Handlers
{
    internal class InlinePipelineHandler<T> : IPipelineHandler<T>
    {
        private readonly Action<T> _action;

        public InlinePipelineHandler(Action<T> action)
        {
            if(action == null)
                throw new ArgumentNullException("action");

            _action = action;
        }

        public void Handle(T input)
        {
            _action.Invoke(input);
        }
    }
}
