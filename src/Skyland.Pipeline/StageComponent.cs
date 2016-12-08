#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Filters;
using Skyland.Pipeline.Handlers;
using Skyland.Pipeline.Impl;
using Skyland.Pipeline.Impl.Filters;
using Skyland.Pipeline.Impl.Handlers;
using Skyland.Pipeline.Internal.Impl;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public sealed class StageComponent<TInput, TOutput>
    {
        /// <summary>
        /// The job
        /// </summary>
        private readonly IPipelineJob<TInput, TOutput> _job;

        /// <summary>
        /// The filters
        /// </summary>
        private readonly IList<IPipelineFilter<TInput>> _filters;
        /// <summary>
        /// The handlers
        /// </summary>
        private readonly IList<IPipelineHandler<TOutput>> _handlers;

        /// <summary>
        /// Prevents a default instance of the <see cref="StageComponent{TInput,TOutput}"/> class from being created.
        /// </summary>
        private StageComponent()
        {
            _filters  = new List<IPipelineFilter<TInput>>();
            _handlers = new List<IPipelineHandler<TOutput>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StageComponent{TInput,TOutput}"/> class.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <exception cref="System.ArgumentNullException">job</exception>
        public StageComponent(IPipelineJob<TInput, TOutput> job)
            :this()
        {
            if(job == null)
                throw new ArgumentNullException("job");

            _job = job;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StageComponent{TInput,TOutput}"/> class.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <exception cref="System.ArgumentNullException">function</exception>
        public StageComponent(Func<TInput, TOutput> function)
            :this()
        {
            if (function == null)
                throw new ArgumentNullException("function");

            _job = new InlineJob<TInput, TOutput>(function);
        }

        /// <summary>
        /// Withes the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">filter</exception>
        public StageComponent<TInput, TOutput> WithFilter(IPipelineFilter<TInput> filter)
        {
            if(filter == null)
                throw new ArgumentNullException("filter");

            _filters.Add(filter);

            return this;
        }

        /// <summary>
        /// Withes the filter.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">function</exception>
        public StageComponent<TInput, TOutput> WithFilter(Func<TInput, bool> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");

            return WithFilter(new InlinePipelineFilter<TInput>(function));
        }

        /// <summary>
        /// Withes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">handler</exception>
        public StageComponent<TInput, TOutput> WithHandler(IPipelineHandler<TOutput> handler)
        {
            if(handler == null)
                throw new ArgumentNullException("handler");

            _handlers.Add(handler);

            return this;
        }

        /// <summary>
        /// Withes the handler.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">action</exception>
        public StageComponent<TInput, TOutput> WithHandler(Action<TOutput> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            return WithHandler(new InlinePipelineHandler<TOutput>(action));
        }

        /// <summary>
        /// Gets the stage.
        /// </summary>
        /// <returns></returns>
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
