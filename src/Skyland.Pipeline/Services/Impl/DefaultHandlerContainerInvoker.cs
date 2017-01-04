#region using

using System.Collections.Generic;
using Skyland.Pipeline.Containers;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline.Services.Impl
{
    internal class DefaultHandlerContainerInvoker : IHandlerExecutionContainersInvoker
    {
        public PipelineOutput<object> Invoke(object obj, IEnumerable<IHandlerExecutionContainer> containers, PipelineErrorHandler errorHandler)
        {
            if (containers == null)
                return new PipelineOutput<object>(OutputStatus.Completed);

            foreach (var handler in containers)
            {
                var output = handler.Execute(obj, errorHandler);
                if (!output.IsCompleted)
                    return new PipelineOutput<object>(output.Status);
            }

            return new PipelineOutput<object>(OutputStatus.Completed);
        }
    }
}
