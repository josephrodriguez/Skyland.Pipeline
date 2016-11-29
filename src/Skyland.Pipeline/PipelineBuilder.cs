#region using

using System;
using Skyland.Pipeline.Handler;
using Skyland.Pipeline.Impl;

#endregion

namespace Skyland.Pipeline
{
    public class PipelineBuilder<TIn, TOut>
    {
        private readonly DefaultPipeline<TIn, TOut> _pipeline;

        public PipelineBuilder()
        {
            _pipeline = new DefaultPipeline<TIn, TOut>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJobIn"></typeparam>
        /// <typeparam name="TJobOut"></typeparam>
        /// <param name="job"></param>
        /// <returns></returns>
        public PipelineBuilder<TIn, TOut> Register<TJobIn, TJobOut>(IPipelineJob<TJobIn, TJobOut> job)
        {
            if(job == null)
                throw new NullReferenceException("Null reference pipeline job instance.");

            //Input type must match with first input job
            if (_pipeline.Count == 0 && typeof(TJobIn) != typeof(TIn))
                throw new Exception("Input of job is not the same of declared pipeline.");

            //Pipeline output type must match with current job input
            if (_pipeline.Count > 0 && _pipeline.OutputType != typeof(TJobIn))
                throw new Exception("Input of current job don´t match with previous job output.");

            _pipeline.RegisterJob(job);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJobInput"></typeparam>
        /// <typeparam name="TJobOutput"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public PipelineBuilder<TIn, TOut> Register<TJobInput, TJobOutput>(Func<TJobInput, TJobOutput> function)
        {
            if (function == null)
                throw new NullReferenceException("Inline function definition.");

            var inlineJob = new InlineJob<TJobInput, TJobOutput>(function);

            return Register(inlineJob);
;        }

        public PipelineBuilder<TIn, TOut> OnError(Action<object, Exception> action)
        {
            _pipeline.OnError += new PipelineErrorHandler(action);
            return this;
        }

        public IPipeline<TIn, TOut> Build()
        {
            if(_pipeline.Count == 0)
                throw new Exception("There is not registered pipeline job.");

            if (_pipeline.OutputType != typeof(TOut))
                throw new Exception("Outout type of current pipeline don´t match with declared pipeline.");

            return _pipeline;
        } 
    }
}
