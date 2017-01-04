using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Containers;
using Skyland.Pipeline.Internal.Interfaces;

namespace Skyland.Pipeline
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
        /// <param name="container">The container.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns></returns>
        PipelineOutput<object> Invoke(object obj, IEnumerable<IHandlerExecutionContainer> container, PipelineErrorHandler errorHandler);
    }
}
