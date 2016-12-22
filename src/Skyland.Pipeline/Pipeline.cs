#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.Internal;
using Skyland.Pipeline.Internal.Components;
using Skyland.Pipeline.Internal.Enums;
using Skyland.Pipeline.Properties;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class Pipeline<TInput, TOutput> : IPipeline<TInput, TOutput>
    {
        private readonly IList<IStageComponent> _stageComponents;

        #region Events

        internal event ErrorHandler OnError;

        #endregion

        private Pipeline()
        {
            _stageComponents = new List<IStageComponent>();
        }

        private void Register(IStageComponent component)
        {
            if(component == null)
                throw new ArgumentNullException(nameof(component));

            _stageComponents.Add(component);
        }

        /// <summary>
        /// Returns the result of invoke all registered stages. All stages jobs are executed in the current thread.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public PipelineResult<TOutput> Execute(TInput input)
        {
            object currentObj = input;

            foreach (var component in _stageComponents)
            {
                var response = component.Execute(currentObj);

                if(response.Status != ResponseStatus.Completed)
                    return new PipelineResult<TOutput>(response.Status);

                currentObj = response.Result;
            }

            return new PipelineResult<TOutput>((TOutput) currentObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Skyland.Pipeline.IPipeline{TInput, TOutput}" />
        public class Builder
        {
            private Type _currentOutputType;
            private ErrorHandler _errorHandler;

            private readonly IList<IStageComponentBuilder> _builders;

            /// <summary>
            /// Initializes a new instance of the <see cref="Builder"/> class.
            /// </summary>
            public Builder()
            {
                _builders = new List<IStageComponentBuilder>();
            }

            /// <summary>
            /// Registers the specified configurator.
            /// </summary>
            /// <typeparam name="Input">The type of the nput.</typeparam>
            /// <typeparam name="Output">The type of the utput.</typeparam>
            /// <param name="configurator">The configurator.</param>
            /// <returns></returns>
            /// <exception cref="Skyland.Pipeline.Exceptions.PipelineException"></exception>
            public Builder Register<Input, Output>(FluentStageConfigurator<Input, Output> configurator)
            {
                if (_builders.Count == 0 && typeof(TInput) != typeof(Input))
                    throw new PipelineException(Resources.Missmatch_TypeInput_Error);

                if(_currentOutputType != null && _currentOutputType != typeof(Input))


                var builder = new StageComponentBuilder<Input, Output>(configurator);

                _builders.Add(builder);
                _currentOutputType = typeof(Output);

                return this;
            }

            /// <summary>
            /// Called when [error].
            /// </summary>
            /// <param name="handler">The handler.</param>
            /// <returns></returns>
            /// <exception cref="System.ArgumentNullException">handler</exception>
            public Builder OnError(ErrorHandler handler)
            {
                if(handler == null)
                    throw new ArgumentNullException(nameof(handler));

                _errorHandler += handler;

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public IPipeline<TInput, TOutput> Build()
            {
                if(_currentOutputType != typeof(TOutput))
                    throw new PipelineException(Resources.Missmatch_TypeOutput_Error);

                var pipeline = new Pipeline<TInput, TOutput>();

                //Create components from builders
                foreach (var builder in _builders)
                {
                    var component = builder.Build();

                    pipeline.Register(component);
                }

                //Register Errorhandler delegates
                if (_errorHandler == null)
                    return pipeline;

                return pipeline;
            }
        }
    }
}
