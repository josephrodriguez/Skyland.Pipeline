#region using

using System.Collections.Generic;
using Skyland.Pipeline.Containers;
using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterExecutionContainerInvoker
    {
        /// <summary>
        /// Invokes the specified containers.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="containers">The containers.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns></returns>
        PipelineOutput<object> Invoke(object obj, IEnumerable<IFilterExecutionContainer> containers, PipelineErrorHandler errorHandler);
    }
}
