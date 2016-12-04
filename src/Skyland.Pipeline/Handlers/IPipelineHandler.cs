namespace Skyland.Pipeline.Handlers
{
    public interface IPipelineHandler<in TOutput>
    {
        void Handle(TOutput output);
    }
}
