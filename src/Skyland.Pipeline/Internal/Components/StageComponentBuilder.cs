#region using

using System;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.Properties;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal class StageComponentBuilder<TInput, TOutput> : IStageComponentBuilder
    {
        private readonly FluentStageConfigurator<TInput, TOutput> _configurator;
         
        public StageComponentBuilder(FluentStageConfigurator<TInput, TOutput> configurator)
        {
            if(configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            _configurator = configurator;
            _configurator += c => c.OnError(null);
        } 

        public IStageComponent Build()
        {
            var configuration = new FluentStageConfiguration<TInput, TOutput>();

            _configurator(configuration);

            if (configuration.JobComponent == null)
                throw new PipelineException(Resources.NoJob_Registered_Error);

            var component = new StageComponent(configuration.JobComponent);

            foreach (var filter in configuration.FilterComponents)
                component.Register(filter);

            foreach (var handler in configuration.HandlerComponents)
                component.Register(handler);

            return component;
        }
    }
}
