using System.Collections.Generic;
using Skyland.Pipeline.Containers;
using Skyland.Pipeline.Delegates;

namespace Skyland.Pipeline.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHandlerExecutionContainersInvoker
    {
        /// <summary>
        /// Executes the specified container.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="containers">The container.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns></returns>
        PipelineOutput<object> Invoke(object obj, IEnumerable<IHandlerExecutionContainer> containers, PipelineErrorHandler errorHandler);
    }
}
