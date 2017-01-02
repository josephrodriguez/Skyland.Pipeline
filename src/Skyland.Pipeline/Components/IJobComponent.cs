namespace Skyland.Pipeline.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IJobComponent<in TInput, out TOutput>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TOutput Execute(TInput input);
    }
}
