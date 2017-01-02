namespace Skyland.Pipeline.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilterComponent<in T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Execute(T element);
    }
}
