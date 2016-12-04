namespace Skyland.Pipeline.Filters
{
    public interface IPipelineFilter<in TInput>
    {
        bool Filter(TInput input);
    }
}
