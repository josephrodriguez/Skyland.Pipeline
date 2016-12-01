namespace Skyland.Pipeline
{
    public interface IPipelineJob<in TInput, out TOutput>
    {
        TOutput Process(TInput input);
    }
}
