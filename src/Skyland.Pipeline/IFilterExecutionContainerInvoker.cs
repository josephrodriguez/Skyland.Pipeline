#region using

using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Interfaces;

#endregion

namespace Skyland.Pipeline
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
