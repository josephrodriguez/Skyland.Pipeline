#region using

using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Internal.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHandlerExecutionContainer
    {
        /// <summary>
        /// Executes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler);
    }
}
