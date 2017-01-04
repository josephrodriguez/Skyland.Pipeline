#region using

using System;
using Skyland.Pipeline.Components.Job;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline.Containers.Impl
{
    internal class JobExecutionContainer<TIn, TOut> : IJobExecutionContainer
    {
        private readonly IJobComponent<TIn, TOut> _job;
         
        public JobExecutionContainer(IJobComponent<TIn, TOut> job)
        {
            _job = job;
        }

        public PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler)
        {
            try
            {
                var output = _job.Execute((TIn) obj);
                return new PipelineOutput<object>(output);
            }
            catch (Exception exception)
            {
                if(handler == null)
                    throw;

                handler(_job, exception);
                return new PipelineOutput<object>(OutputStatus.Error);
            }
        }
    }
}
