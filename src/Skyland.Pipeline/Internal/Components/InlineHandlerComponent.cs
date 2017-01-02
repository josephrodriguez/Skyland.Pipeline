#region using

using System;
using Skyland.Pipeline.Components;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal class InlineHandlerComponent<T> : IHandlerComponent<T>
    {
        private readonly Action<T> _action;

        public InlineHandlerComponent(Action<T> action)
        {
            _action = action;
        }

        public void Handle(T obj)
        {
            _action.Invoke(obj);
        }
    }
}
