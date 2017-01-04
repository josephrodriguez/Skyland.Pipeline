namespace Skyland.Pipeline.Components.Handlers
{
    /// <summary>
    /// Represent a handler component which handle the output object of job component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandlerComponent<in T>
    {
        /// <summary>
        /// Perform an action on output result of job component.
        /// </summary>
        /// <param name="obj"></param>
        void Handle(T obj);
    }
}
