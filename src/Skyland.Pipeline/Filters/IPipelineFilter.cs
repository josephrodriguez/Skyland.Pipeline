namespace Skyland.Pipeline.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    public interface IPipelineFilter<in TInput>
    {
        /// <summary>
        /// Filters the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        bool Filter(TInput input);
    }
}
