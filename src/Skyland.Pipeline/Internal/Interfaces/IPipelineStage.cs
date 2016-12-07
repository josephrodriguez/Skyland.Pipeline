namespace Skyland.Pipeline.Internal.Interfaces
{
    internal interface IPipelineStage<in TInput, TOutput>
    {
        PipelineResult<TOutput> Execute(TInput input);
    }
}
