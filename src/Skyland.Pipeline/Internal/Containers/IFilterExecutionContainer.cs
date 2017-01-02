#region using

using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Internal.Containers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterExecutionContainer
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
