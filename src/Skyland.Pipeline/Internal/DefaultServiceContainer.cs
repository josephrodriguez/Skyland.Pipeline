#region using

using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline.Internal
{
    internal class DefaultServiceContainer : ServiceContainer
    {
        public DefaultServiceContainer()
        {
            SetSingle<IHandlerExecutionContainersInvoker>(new DefaultHandlerContainerInvoker());
            SetSingle<IFilterExecutionContainerInvoker>(new DefaultFilterContainerInvoker());
            SetSingle<IJobExecutionContainerInvoker>(new DefaultJobContainerInvoker());
            SetSingle<PipelineErrorHandler>(null);
        }
    }
}
