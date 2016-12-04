#region using

using System;
using System.Linq;
using System.Reflection;
using Skyland.Pipeline.Handler;
using Skyland.Pipeline.Impl;

#endregion

namespace Skyland.Pipeline
{
    public class PipelineBuilder<TInput, TOutput>
    {
        private readonly DefaultPipeline<TInput, TOutput> _pipeline;

        public PipelineBuilder()
        {
            _pipeline = new DefaultPipeline<TInput, TOutput>();
        }

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

        public PipelineBuilder<TInput, TOutput> OnError(Action<object, Exception> action)
        {
            _pipeline.OnError += new PipelineErrorHandler(action);
            return this;
        }

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
