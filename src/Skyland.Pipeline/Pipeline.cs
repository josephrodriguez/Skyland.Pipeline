﻿#region using

using System;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Exceptions;
using Skyland.Pipeline.Internal.Components;
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
        private readonly IList<IStageComponent> _stages;

        private PipelineErrorHandler _errorHandler;

        private Pipeline()
        {
            _stages = new List<IStageComponent>();
        }

        private void Register(IStageComponent stageComponent)
        {
            _stages.Add(stageComponent);
        }

        private void Register(PipelineErrorHandler errorHandler)
        {
            _errorHandler += errorHandler;
        }

        /// <summary>
        /// Returns the result of invoke all registered stages. All stages jobs are executed in the current thread.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public PipelineOutput<TOutput> Execute(TInput input)
        {
            object currentObj = input;

            foreach (var stage in _stages)
            {
                var output = stage.Execute(currentObj, _errorHandler);
                if (output.IsCompleted)
                    currentObj = output.Result;
                else
                    return new PipelineOutput<TOutput>(output.Status);
            }

            return new PipelineOutput<TOutput>((TOutput) currentObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Skyland.Pipeline.IPipeline{TInput, TOutput}" />
        public class Builder
        {
            private Type _currentOutputType;

            private PipelineErrorHandler _errorHandler;

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
            /// <typeparam name="Input">The type of the input.</typeparam>
            /// <typeparam name="Output">The type of the output.</typeparam>
            /// <param name="configurator">The configurator.</param>
            /// <returns></returns>
            /// <exception cref="Skyland.Pipeline.Exceptions.PipelineException"></exception>
            public Builder Register<Input, Output>(FluentStageConfigurator<Input, Output> configurator)
            {
                if (_builders.Count == 0 && !typeof(Input).IsAssignableFrom(typeof(TInput)))
                    throw new PipelineException(Resources.Missmatch_TypeInput_Error);

                if(_currentOutputType != null && !typeof(Input).IsAssignableFrom(_currentOutputType))
                    throw new PipelineException(Resources.Missmatch_LastRegisteredComponent_Error);

                var builder = new StageComponentBuilder<Input, Output>(configurator);
                _builders.Add(builder);

                _currentOutputType = typeof(Output);

                return this;
            }

            /// <summary>
            /// Called when [error].
            /// </summary>
            /// <param name="errorHandler"></param>
            /// <returns></returns>
            /// <exception cref="System.ArgumentNullException">handler</exception>
            public Builder OnError(PipelineErrorHandler errorHandler)
            {
                if(errorHandler == null)
                    throw new ArgumentNullException(nameof(errorHandler));

                //Subscribe error handler for current builders
                _errorHandler += errorHandler;

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public IPipeline<TInput, TOutput> Build()
            {
                if(_currentOutputType == null)
                    throw new PipelineException(Resources.Pipeline_WithoutRegisteredComponent_Error);

                if(_currentOutputType != typeof(TOutput))
                    throw new PipelineException(Resources.Missmatch_TypeOutput_Error);

                var pipeline = new Pipeline<TInput, TOutput>();

                //Create components from builders
                foreach (var builder in _builders)
                {
                    var stage = builder.Build();
                    pipeline.Register(stage);
                }

                if(_errorHandler != null)
                    pipeline.Register(_errorHandler);

                return pipeline;
            }
        }
    }
}
