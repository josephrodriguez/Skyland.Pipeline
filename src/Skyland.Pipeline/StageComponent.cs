#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Filters;
using Skyland.Pipeline.Handlers;
using Skyland.Pipeline.Impl;
using Skyland.Pipeline.Impl.Filters;
using Skyland.Pipeline.Impl.Handlers;

#endregion

namespace Skyland.Pipeline
{
    public sealed class StageComponent<TInput, TOutput>
    {
        private readonly IPipelineJob<TInput, TOutput> _job;

        private readonly IList<IPipelineFilter<TInput>> _filters;
        private readonly IList<IPipelineHandler<TOutput>> _handlers; 
         
        internal StageComponent(IPipelineJob<TInput, TOutput> job)
        {
            if(job == null)
                throw new ArgumentNullException("job");

            _job = job;

            _filters = new List<IPipelineFilter<TInput>>();
            _handlers = new List<IPipelineHandler<TOutput>>();
        }

        public StageComponent<TInput, TOutput> WithFilter(IPipelineFilter<TInput> filter)
        {
            if(filter == null)
                throw new ArgumentNullException("filter");

            _filters.Add(filter);

            return this;
        }

        public StageComponent<TInput, TOutput> WithFilter(Func<TInput, bool> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");

            return WithFilter(new InlinePipelineFilter<TInput>(function));
        }

        public StageComponent<TInput, TOutput> WithHandler(IPipelineHandler<TOutput> handler)
        {
            if(handler == null)
                throw new ArgumentNullException("handler");

            _handlers.Add(handler);

            return this;
        }

        public StageComponent<TInput, TOutput> WithHandler(Action<TOutput> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            return WithHandler(new InlinePipelineHandler<TOutput>(action));
        }

        internal PipelineStage<TInput, TOutput> GetStage()
        {
            var stage = new PipelineStage<TInput, TOutput>(_job);

            //Register filters
            foreach (var filter in _filters)
                stage.Register(filter);

            //Register handlers
            foreach (var handler in _handlers)
                stage.Register(handler);

            return stage;
        } 
    }
}
