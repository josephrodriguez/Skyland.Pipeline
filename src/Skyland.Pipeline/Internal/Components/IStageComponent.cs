using Skyland.Pipeline.Delegates;

namespace Skyland.Pipeline.Internal.Components
{
    internal interface IStageComponent
    {
        ComponentResponse Execute(object input);

        void Register(ErrorHandler handler);
    }
}
