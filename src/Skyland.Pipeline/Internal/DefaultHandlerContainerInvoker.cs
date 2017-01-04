#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Enums;
using Skyland.Pipeline.Internal.Interfaces;

#endregion

namespace Skyland.Pipeline.Internal
{
    internal class DefaultHandlerContainerInvoker : IHandlerExecutionContainersInvoker
    {
        public PipelineOutput<object> Invoke(object obj, IEnumerable<IHandlerExecutionContainer> handlers, PipelineErrorHandler errorHandler)
        {
            if (handlers == null)
                return new PipelineOutput<object>(OutputStatus.Completed);

            foreach (var handler in handlers)
            {
                var output = handler.Execute(obj, errorHandler);
                if (!output.IsCompleted)
                    return new PipelineOutput<object>(output.Status);
            }

            return new PipelineOutput<object>(OutputStatus.Completed);
        }
    }
}
