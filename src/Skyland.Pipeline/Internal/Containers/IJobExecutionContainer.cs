#region using

using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Internal.Containers
{
    internal interface IJobExecutionContainer
    {
        PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler);
    }
}
