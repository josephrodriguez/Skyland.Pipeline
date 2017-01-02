namespace Skyland.Pipeline
{
    /// <summary>
    /// Synchronous pipeline where all registered stages are executed sequentially
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public interface IPipeline<in TInput, TOutput>
    {
        #region Methods

        /// <summary>
        /// Returns the result of invoke all registered stages. All stages jobs are executed in the current thread.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        PipelineOutput<TOutput> Execute(TInput input);

        #endregion
    }
}
