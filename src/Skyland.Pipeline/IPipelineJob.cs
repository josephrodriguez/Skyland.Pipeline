namespace Skyland.Pipeline
{
    /// <summary>
    /// Abstract component that define a processing of input parameter and return an output
    /// </summary>
    /// <typeparam name="TInput">The type of the job input.</typeparam>
    /// <typeparam name="TOutput">The type of the job output.</typeparam>
    public interface IPipelineJob<in TInput, out TOutput>
    {
        /// <summary>
        /// Processes the specified parameter input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        TOutput Process(TInput input);
    }
}
