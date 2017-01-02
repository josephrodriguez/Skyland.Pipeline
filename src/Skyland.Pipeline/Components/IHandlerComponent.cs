namespace Skyland.Pipeline.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandlerComponent<in T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handledObj"></param>
        void Handle(T handledObj);
    }
}
