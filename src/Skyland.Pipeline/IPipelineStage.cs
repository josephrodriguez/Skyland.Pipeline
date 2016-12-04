namespace Skyland.Pipeline
{
    public interface IPipelineStage<in TInput, out TOutput>
    {
        TOutput Execute(TInput input);
    }
}
