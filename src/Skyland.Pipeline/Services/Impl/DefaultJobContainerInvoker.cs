#region using

using Skyland.Pipeline.Containers;
using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Services.Impl
{
    internal class DefaultJobContainerInvoker : IJobExecutionContainerInvoker
    {
        public PipelineOutput<object> Invoke(object obj, IJobExecutionContainer jobContainer, PipelineErrorHandler errorHandler)
        {
            return jobContainer.Execute(obj, errorHandler);
        }
    }
}
