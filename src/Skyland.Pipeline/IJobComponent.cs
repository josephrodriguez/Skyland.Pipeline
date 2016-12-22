namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJobComponent : IComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        object Execute(object obj);
    }
}
