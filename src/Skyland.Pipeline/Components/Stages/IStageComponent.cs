using Skyland.Pipeline.Services;

namespace Skyland.Pipeline.Components.Stages
{
    internal interface IStageComponent
    {
        PipelineOutput<object> Execute(object input, ServiceContainer services);
    }
}
