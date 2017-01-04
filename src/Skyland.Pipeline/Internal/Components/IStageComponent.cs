using Skyland.Pipeline.Delegates;

namespace Skyland.Pipeline.Internal.Components
{
    internal interface IStageComponent
    {
        PipelineOutput<object> Execute(object input, ServiceContainer services);
    }
}
