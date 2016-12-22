namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandlerComponent<in T> : IComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void Handle(T element);
    }
}
