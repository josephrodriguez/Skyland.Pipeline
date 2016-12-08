#region using

using System;
using System.Linq;
using System.Reflection;
using Skyland.Pipeline.Handlers;
using Skyland.Pipeline.Internal.Impl;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public class PipelineBuilder<TInput, TOutput>
    {
        /// <summary>
        /// The pipeline
        /// </summary>
        private readonly Pipeline<TInput, TOutput> _pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineBuilder{TInput, TOutput}"/> class.
        /// </summary>
        public PipelineBuilder()
        {
            _pipeline = new Pipeline<TInput, TOutput>();
        }

        /// <summary>
        /// Registers the specified stage on pipeline.
        /// </summary>
        /// <typeparam name="TIn">The input type of stage.</typeparam>
        /// <typeparam name="TOut">The output type of stage.</typeparam>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">component</exception>
        /// <exception cref="System.Exception">
        /// Input of job is not the same of declared pipeline.
        /// or
        /// Input of current job don´t match with previous job output.
        /// </exception>
        public PipelineBuilder<TInput, TOutput> Register<TIn, TOut>(StageComponent<TIn, TOut> component)
        {
            if(component == null)
                throw new ArgumentNullException("component");

            //Input type must match with first input job
            if (_pipeline.Count == 0 && typeof(TIn) != typeof(TInput))
                throw new Exception("Input of job is not the same of declared pipeline.");

            //Pipeline output type must match with current job input
            if (_pipeline.Count > 0 && _pipeline.OutputType != typeof(TIn))
                throw new Exception("Input of current job don´t match with previous job output.");

            var stage = component.GetStage();

            _pipeline.RegisterStage(stage);

            return this;
        }

        /// <summary>
        /// Registers the specified components.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">components</exception>
        /// <exception cref="System.MissingMethodException"></exception>
        /// <exception cref="System.NullReferenceException">Declared stage component is null.</exception>
        /// <exception cref="System.InvalidOperationException">
        /// Stage component is not generic type.
        /// or
        /// Specific component don´t implement a required type.
        /// </exception>
        public PipelineBuilder<TInput, TOutput> Register(params object[] components)
        {
            if(components == null)
                throw new ArgumentNullException("components");

            if (components.Length == 0)
                return this;

            var methodName = MethodBase.GetCurrentMethod().Name;

            var methodInfo =
                GetType()
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == methodName && m.IsGenericMethod);

            if(methodInfo == null)
                throw new MissingMethodException("");

            foreach (var component in components)
            {
                if(component == null)
                    throw new NullReferenceException("Declared stage component is null.");

                var genericType = component.GetType().GetGenericTypeDefinition();
                if(genericType == null)
                    throw new InvalidOperationException("Stage component is not generic type.");

                if (genericType != typeof (StageComponent<,>))
                    throw new InvalidOperationException("Specific component don´t implement a required type.");

                var arguments = component.GetType().GetGenericArguments();

                var genericMethod = methodInfo.MakeGenericMethod(arguments[0], arguments[1]);
                genericMethod.Invoke(this, new[] {component});
            }

            return this;
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public PipelineBuilder<TInput, TOutput> OnError(Action<object, Exception> action)
        {
            _pipeline.OnError += new PipelineErrorHandler(action);
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// There is not registered pipeline job.
        /// or
        /// Outout type of current pipeline don´t match with declared pipeline.
        /// </exception>
        public IPipeline<TInput, TOutput> Build()
        {
            if(_pipeline.Count == 0)
                throw new Exception("There is not registered pipeline job.");

            if (_pipeline.OutputType != typeof(TOutput))
                throw new Exception("Outout type of current pipeline don´t match with declared pipeline.");

            return _pipeline;
        } 
    }
}
