#region using

using System.Collections.Generic;
using Skyland.Pipeline.Containers;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline.Services.Impl
{
    internal class DefaultFilterContainerInvoker : IFilterExecutionContainerInvoker
    {
        public PipelineOutput<object> Invoke(object obj, IEnumerable<IFilterExecutionContainer> filters, PipelineErrorHandler errorHandler)
        {
            if (filters == null)
                return new PipelineOutput<object>(OutputStatus.Completed);

            foreach (var filter in filters)
            {
                var output = filter.Execute(obj, errorHandler);
                if (!output.IsCompleted)
                    return new PipelineOutput<object>(output.Status);
            }

            return new PipelineOutput<object>(OutputStatus.Completed);
        }
    }
}
