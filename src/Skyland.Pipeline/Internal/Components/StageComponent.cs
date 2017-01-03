#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Containers;
using Skyland.Pipeline.Internal.Enums;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal sealed class StageComponent : IStageComponent
    {
        private IList<IFilterExecutionContainer> _filterContainers;
        private IList<IHandlerExecutionContainer> _handlerContainers; 
        private readonly IJobExecutionContainer _jobExecutionContainer;

        public StageComponent(IJobExecutionContainer container)
        {
            _jobExecutionContainer = container;
        }

        public void Register(IFilterExecutionContainer filterExecutionContainer)
        {
            if(_filterContainers == null)
                _filterContainers = new List<IFilterExecutionContainer>();

            _filterContainers.Add(filterExecutionContainer);
        }

        public void Register(IHandlerExecutionContainer handlerExecutionContainer)
        {
            if (_handlerContainers == null)
                _handlerContainers = new List<IHandlerExecutionContainer>();

            _handlerContainers.Add(handlerExecutionContainer);
        }

        public PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler)
        {
            //Execute filters
            var output = ExecuteFilters(obj, handler);
            if(!output.IsCompleted)
                return output;

            //Execute job
            output = _jobExecutionContainer.Execute(obj, handler);
            if (!output.IsCompleted)
                return output;

            //Execute handlers
            var handlersOutput = ExecuteHandlers(output.Result, handler);

            return 
                handlersOutput.IsCompleted 
                    ? output
                    : handlersOutput;
        }

        private PipelineOutput<object> ExecuteFilters(object obj, PipelineErrorHandler errorHandler)
        {
            if(_filterContainers == null)
                return new PipelineOutput<object>(OutputStatus.Completed);

            foreach (var filterContainer in _filterContainers)
            {
                var output = filterContainer.Execute(obj, errorHandler);
                if (!output.IsCompleted)
                    return new PipelineOutput<object>(output.Status);
            }

            return new PipelineOutput<object>(OutputStatus.Completed);
        }

        private PipelineOutput<object> ExecuteHandlers(object obj, PipelineErrorHandler errorHandler)
        {
            if(_handlerContainers == null)
                return new PipelineOutput<object>(OutputStatus.Completed);

            foreach (var handlerContainer in _handlerContainers)
            {
                var output = handlerContainer.Execute(obj, errorHandler);
                if (!output.IsCompleted)
                    return new PipelineOutput<object>(output.Status);
            }

            return new PipelineOutput<object>(OutputStatus.Completed);
        }
    }
}
