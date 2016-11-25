namespace Skyland.Pipeline
{
    public interface IPipeline<in TIn, out TOut>
    {
        TOut Execute(TIn input);
    }
}
