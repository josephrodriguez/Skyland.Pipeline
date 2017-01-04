#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.Extensions;
using Skyland.Pipeline.Internal.Containers;
using Skyland.Pipeline.Internal.Enums;
using Skyland.Pipeline.Internal.Interfaces;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal sealed class StageComponent : IStageComponent
    {
        private IList<IFilterExecutionContainer> _filters;
        private IList<IHandlerExecutionContainer> _handlers; 
        private readonly IJobExecutionContainer _jobContainer;

        public StageComponent(IJobExecutionContainer jobContainerContainer)
        {
            _jobContainer = jobContainerContainer;
        }

        public void Register(IFilterExecutionContainer filterExecutionContainer)
        {
            if(_filters == null)
                _filters = new List<IFilterExecutionContainer>();

            _filters.Add(filterExecutionContainer);
        }

        public void Register(IHandlerExecutionContainer handlerExecutionContainer)
        {
            if (_handlers == null)
                _handlers = new List<IHandlerExecutionContainer>();

            _handlers.Add(handlerExecutionContainer);
        }

        public PipelineOutput<object> Execute(object obj, ServiceContainer services)
        {
            //Get filter invoker and eror handler from service container
            var filterInvokerService = services.GetFilterContainerInvoker();
            var pipelineErrorHandler = services.GetErrorHandler();

            //Invoke filters
            var output = filterInvokerService.Invoke(obj, _filters, pipelineErrorHandler);
            if(!output.IsCompleted)
                return output;

            //Get job invoker from service container
            var jobInvoker = services.GetJobContainerInvoker();

            //Invoke job container
            output = jobInvoker.Invoke(obj, _jobContainer, pipelineErrorHandler);
            if (!output.IsCompleted)
                return output;

            //Get handlers invoker from service container
            var handlerInvokerService = services.GetHandlerContainerInvoker();

            //Invoke handlers
            var handlersOutput = handlerInvokerService.Invoke(output.Result, _handlers, pipelineErrorHandler);

            return 
                handlersOutput.IsCompleted 
                    ? output
                    : handlersOutput;
        }
    }
}
