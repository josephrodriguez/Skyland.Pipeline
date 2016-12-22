#region using

#endregion

namespace Skyland.Pipeline.Internal.Decorators
{
    internal class HandlerComponentDecorator<TOut> : IHandlerComponent
    {
        private readonly IHandlerComponent<TOut> _handler;

        public HandlerComponentDecorator(IHandlerComponent<TOut> handler)
        {
            _handler = handler;
        }

        public void Handle(object obj)
        {
            _handler.Handle((TOut) obj);
        }
    }
}
