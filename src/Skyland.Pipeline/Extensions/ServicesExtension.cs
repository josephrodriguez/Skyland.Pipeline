#region using

using System;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Services;

#endregion

namespace Skyland.Pipeline.Extensions
{
    internal static class ServicesExtension
    {
        private static TService GetRequiredService<TService>(this ServiceContainer container)
        {
            if(container == null)
                throw new ArgumentNullException(nameof(container));

            var service = container.GetService(typeof (TService));
            if(service == null)
                throw new Exception("Service container instance don´t contain a registered instance of requested service.");

            return (TService) service;
        }

        private static TService GetOptionalService<TService>(this ServiceContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            var service = container.GetService(typeof(TService));
            return (TService)service;
        }

        internal static IFilterExecutionContainerInvoker GetFilterContainerInvoker(this ServiceContainer container)
        {
            return container.GetRequiredService<IFilterExecutionContainerInvoker>();
        }

        internal static IJobExecutionContainerInvoker GetJobContainerInvoker(this ServiceContainer container)
        {
            return container.GetRequiredService<IJobExecutionContainerInvoker>();
        }

        internal static IHandlerExecutionContainersInvoker GetHandlerContainerInvoker(this ServiceContainer container)
        {
            return container.GetRequiredService<IHandlerExecutionContainersInvoker>();
        }

        internal static PipelineErrorHandler GetErrorHandler(this ServiceContainer container)
        {
            return container.GetOptionalService<PipelineErrorHandler>();
        }
    }
}
