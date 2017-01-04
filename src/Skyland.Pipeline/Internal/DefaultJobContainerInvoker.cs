#region using

using System;
using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Internal
{
    internal class DefaultJobContainerInvoker : IJobExecutionContainerInvoker
    {
        public PipelineOutput<object> Invoke(object obj, IJobExecutionContainer jobContainer, PipelineErrorHandler errorHandler)
        {
            return jobContainer.Execute(obj, errorHandler);
        }
    }
}
