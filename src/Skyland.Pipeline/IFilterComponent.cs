namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IFilterComponent : IComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Execute(object obj);
    }
}
