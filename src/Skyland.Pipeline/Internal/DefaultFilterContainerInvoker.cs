#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Enums;
using Skyland.Pipeline.Internal.Interfaces;

#endregion

namespace Skyland.Pipeline.Internal
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
