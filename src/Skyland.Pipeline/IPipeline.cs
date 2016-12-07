namespace Skyland.Pipeline
{
    public interface IPipeline<in TInput, TOutput>
    {
        #region Methods

        /// <summary>
        /// Execute in current thread the parameter on inline jobs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PipelineResult<TOutput> Execute(TInput input);

        #endregion
    }
}
