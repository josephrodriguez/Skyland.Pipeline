namespace Skyland.Pipeline.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public interface IPipelineHandler<in TOutput>
    {
        /// <summary>
        /// Handles the specified output of job.
        /// </summary>
        /// <param name="output">The output.</param>
        void Handle(TOutput output);
    }
}
