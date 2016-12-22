#region using

using System;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal class InlineHandlerComponent<T> : IHandlerComponent<T>
    {
        private readonly Action<T> _action;

        public InlineHandlerComponent(Action<T> action)
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
