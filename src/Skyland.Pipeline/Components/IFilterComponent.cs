namespace Skyland.Pipeline.Components
{
    /// <summary>
    /// Represent a filter component which handle the input object of stage before be proccessed for job component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilterComponent<in T>
    {
        /// <summary>
        /// Return true if current input can be proccessed by job component, false otherwise.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        bool Execute(T element);
    }
}
