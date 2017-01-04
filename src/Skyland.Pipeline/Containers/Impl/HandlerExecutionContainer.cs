﻿#region using

using System;
using Skyland.Pipeline.Components.Handlers;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline.Containers.Impl
{
    internal class HandlerExecutionContainer<TOut> : IHandlerExecutionContainer
    {
        private readonly IHandlerComponent<TOut> _handler;

        public HandlerExecutionContainer(IHandlerComponent<TOut> handler)
        {
            _handler = handler;
        }

        public PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler)
        {
            try
            {
                _handler.Handle((TOut)obj);
                return new PipelineOutput<object>(OutputStatus.Completed);
            }
            catch (Exception exception)
            {
                if (handler == null)
                    throw;

                handler(_handler, exception);
                return new PipelineOutput<object>(OutputStatus.Error);
            }
        }
    }
}
