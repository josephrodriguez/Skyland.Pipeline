namespace Skyland.Pipeline
{
    internal interface IHandlerComponent : IComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void Handle(object obj);
    }
}
