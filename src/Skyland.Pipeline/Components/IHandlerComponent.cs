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
        /// <param name="element"></param>
        void Handle(T element);
    }
}
