#region using

using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Internal.Containers
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IHandlerExecutionContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler);
    }
}
