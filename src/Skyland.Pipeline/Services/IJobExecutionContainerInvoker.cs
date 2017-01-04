using Skyland.Pipeline.Containers;
using Skyland.Pipeline.Delegates;

namespace Skyland.Pipeline.Services
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
