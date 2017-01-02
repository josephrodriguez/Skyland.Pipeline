namespace Skyland.Pipeline.Components
{
    /// <summary>
    /// Represent the component that perform the processing of <see><cref>TInput</cref></see> object on  <see><cref>TOutput</cref></see> object.
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
