#region using

using System;
using System.Collections.Generic;
using System.Linq;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.Internal.Components;
using Skyland.Pipeline.Internal.Decorators;
using Skyland.Pipeline.Properties;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class FluentStageConfiguration<TInput, TOutput>
    {
        private IList<IFilterComponent> _filters;
        private IList<IHandlerComponent> _handlers;
        private IJobComponent _job;
        private ErrorHandler _errorHandler;

        internal IEnumerable<IFilterComponent> FilterComponents
        {
            get { return _filters ?? Enumerable.Empty<IFilterComponent>();}
        }

        internal IJobComponent JobComponent
        {
            get { return _job; }
        }

        internal IEnumerable<IHandlerComponent> HandlerComponents
        {
            get { return _handlers ?? Enumerable.Empty<IHandlerComponent>();}
        } 

        /// <summary>
        /// Filters the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> Filter(IFilterComponent<TInput> filter)
        {
            if(filter == null)
                throw new ArgumentNullException(nameof(filter));

            if(_job != null)
                throw new PipelineException(Resources.Register_Filter_Error);

            var decorator = new FilterComponentDecorator<TInput>(filter);

            if (_filters == null)
                _filters = new List<IFilterComponent>();

            _filters.Add(decorator);

            return this;
        }

        /// <summary>
        /// Filters the specified filter function.
        /// </summary>
        /// <param name="function">The filter function.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">filterFunction</exception>
        public FluentStageConfiguration<TInput, TOutput> Filter(Func<TInput, bool> function)
        {
            if(function == null)
                throw new ArgumentNullException(nameof(function));

            var component = new InlineFilterComponent<TInput>(function);

            return Filter(component);
        }

        /// <summary>
        /// Filters this instance.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> Filter<TFilter>()
            where TFilter : IFilterComponent<TInput>
        {
            var component = Activator.CreateInstance<TFilter>();

            return Filter(component);
        }

        /// <summary>
        /// Jobs the specified job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> Job(IJobComponent<TInput, TOutput> job)
        {
            if(_job != null)
                throw new PipelineException(Resources.JobComponent_Registered_Error);

            _job = new JobComponentDecorator<TInput, TOutput>(job);

            return this;
        }

        /// <summary>
        /// Jobs the specified job function.
        /// </summary>
        /// <param name="jobFunction">The job function.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">jobFunction</exception>
        public FluentStageConfiguration<TInput, TOutput> Job(Func<TInput, TOutput> jobFunction)
        {
            if(jobFunction == null)
                throw new ArgumentNullException(nameof(jobFunction));

            var jobComponent = new InlineJobComponent<TInput, TOutput>(jobFunction);
            return Job(jobComponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> Job<TJob>() 
            where TJob : IJobComponent<TInput, TOutput>
        {
            var component = Activator.CreateInstance<TJob>();

            return Job(component);
        }

        /// <summary>
        /// Handlers the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> Handler(IHandlerComponent<TOutput> handler)
        {
            if(handler == null)
                throw new ArgumentNullException(nameof(handler));

            if(_job == null)
                throw new PipelineException(Resources.Register_Handler_Error);

            var component = new HandlerComponentDecorator<TOutput>(handler);

            if (_handlers == null)
                _handlers = new List<IHandlerComponent>();

            _handlers.Add(component);

            return this;
        }

        /// <summary>
        /// Handlers the specified handler action.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">handlerAction</exception>
        public FluentStageConfiguration<TInput, TOutput> Handler(Action<TOutput> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var handlerComponent = new InlineHandlerComponent<TOutput>(action);

            return Handler(handlerComponent);
        }

        /// <summary>
        /// Handlers this instance.
        /// </summary>
        /// <typeparam name="THandler">The type of the handler.</typeparam>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> Handler<THandler>()
            where THandler : IHandlerComponent<TOutput>
        {
            var handlerComponent = Activator.CreateInstance<THandler>();

            return Handler(handlerComponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorHandler"></param>
        /// <returns></returns>
        public FluentStageConfiguration<TInput, TOutput> OnError(ErrorHandler errorHandler)
        {
            _errorHandler += errorHandler;
            return this;
        }
    }
}
