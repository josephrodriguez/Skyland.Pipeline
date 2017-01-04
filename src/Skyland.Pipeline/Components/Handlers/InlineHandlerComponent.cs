#region using

using System;

#endregion

namespace Skyland.Pipeline.Components.Handlers
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
