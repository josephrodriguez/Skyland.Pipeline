#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Enums;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal sealed class StageComponent : IStageComponent
    {
        private IList<IFilterComponent> _filters;
        private IList<IHandlerComponent> _handlers; 
        private readonly IJobComponent _job;

        private ErrorHandler _errorHandler;

        public StageComponent(IJobComponent job)
        {
            if(job == null)
                throw new ArgumentNullException(nameof(job));

            _job = job;
        }

        public void Register(IFilterComponent filter)
        {
            if(filter == null)
                throw new ArgumentNullException(nameof(filter));

            if(_filters == null)
                _filters = new List<IFilterComponent>();

            _filters.Add(filter);
        }

        public void Register(IHandlerComponent handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            if (_handlers == null)
                _handlers = new List<IHandlerComponent>();

            _handlers.Add(handler);
        }

        public void Register(ErrorHandler handler)
        {
            if(handler == null)
                throw new ArgumentNullException(nameof(handler));

            _errorHandler += handler;
        }

        public ComponentResponse Execute(object input)
        {
            var filtersResponse = ExecuteFilters(input);
            if (filtersResponse.Status != ResponseStatus.Completed)
                return filtersResponse;

            var jobResponse = ExecuteJob(input);
            if (jobResponse.Status != ResponseStatus.Completed)
                return jobResponse;
           
            //Handlers
            var handlersResponse = ExecuteHandlers(jobResponse.Result);

            return
                handlersResponse.Status != ResponseStatus.Completed 
                    ? handlersResponse
                    : jobResponse;
        }

        private ComponentResponse ExecuteFilters(object obj)
        {
            if (_filters == null)
                return ComponentResponse.Completed;

            //Invoke filters
            foreach (var filter in _filters)
            {
                try {
                    var result = filter.Execute(obj);
                    if (result)
                        continue;

                    return ComponentResponse.Rejected;
                }
                catch (Exception exception) {
                    if (_errorHandler == null)
                        throw;

                    _errorHandler(filter, exception);
                    return ComponentResponse.Error;
                }
            }

            return ComponentResponse.Completed;
        }

        private ComponentResponse ExecuteJob(object obj)
        {
            try
            {
                var result = _job.Execute(obj);

                return new ComponentResponse(result);
            }
            catch (Exception exception) {
                if (_errorHandler == null)
                    throw;

                _errorHandler(_job, exception);

                return ComponentResponse.Error;
            }
        }

        private ComponentResponse ExecuteHandlers(object obj)
        {
            if(_handlers == null)
                return ComponentResponse.Completed;

            foreach (var handler in _handlers)
            {
                try {
                    handler.Handle(obj);
                }
                catch (Exception exception) {
                    if (_errorHandler == null)
                        throw;

                    _errorHandler(handler, exception);
                    return ComponentResponse.Error;
                }
            }

            return ComponentResponse.Completed;
        }
    }
}
