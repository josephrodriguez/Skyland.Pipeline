﻿#region using

using System;
using System.Collections.Generic;
using System.Linq;
using Skyland.Pipeline.Enums;
using Skyland.Pipeline.Filters;
using Skyland.Pipeline.Handlers;
using Skyland.Pipeline.Internal.Interfaces;

#endregion

namespace Skyland.Pipeline.Internal.Impl
{
    internal class PipelineStage<TInput, TOutput> : IPipelineStage<TInput, TOutput>
    {
        private readonly IPipelineJob<TInput, TOutput> _job; 
        private readonly IList<IPipelineFilter<TInput>> _filters;
        private readonly IList<IPipelineHandler<TOutput>> _handlers;

        public PipelineStage(IPipelineJob<TInput, TOutput> job)
        {
            if(job == null)
                throw new ArgumentNullException("job");

            _job = job;
            _filters  = new List<IPipelineFilter<TInput>>();
            _handlers = new List<IPipelineHandler<TOutput>>();
        }

        public PipelineResult<TOutput> Execute(TInput input)
        {
            //Invoke registered input handlers
            if (_filters.Any(filter => !filter.Filter(input)))
                return new PipelineResult<TOutput>(Status.Rejected);

            var output = _job.Process(input);

            //Invoke registered output handlers
            foreach (var handler in _handlers)
                handler.Handle(output);

            return 
                new PipelineResult<TOutput>(output);
        }

        public void Register(IPipelineFilter<TInput> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            _filters.Add(filter);
        }

        public void Register(IPipelineHandler<TOutput> handler)
        {
            if(handler == null)
                throw new ArgumentNullException("handler");

            _handlers.Add(handler);
        }
    }
}
