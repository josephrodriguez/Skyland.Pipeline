namespace Skyland.Pipeline
{
    public interface IPipelineJob<in TIn, out TOut>
    {
        TOut Process(TIn input);
    }
}
