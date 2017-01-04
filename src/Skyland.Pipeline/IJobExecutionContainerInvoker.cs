using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Interfaces;

namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJobExecutionContainerInvoker
    {
        /// <summary>
        /// Invokes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="jobContainer">The job container.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns></returns>
        PipelineOutput<object> Invoke(object obj, IJobExecutionContainer jobContainer, PipelineErrorHandler errorHandler);
    }
}
