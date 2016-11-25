#region using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Skyland.Pipeline.Impl
{
    internal class DefaultPipeline<TIn, TOut> : IPipeline<TIn, TOut>
    {
        private readonly IList<object> _jobs;

        public DefaultPipeline() {
            _jobs = new List<object>();
        }

        public int Count {
            get { return _jobs.Count; } 
        }

        public Type OutputType
        {
            get
            {
                var lastJob = _jobs.LastOrDefault();
                if(lastJob == null)
                    return null;

                var jobInterface = lastJob.GetType()
                    .GetInterfaces()
                    .Single(
                        i => 
                            i.GetGenericTypeDefinition() == typeof(IPipelineJob<,>));

                if(jobInterface == null)
                    throw new Exception("Job instance don´t implement required interface.");

                var arguments = jobInterface.GetGenericArguments();

                return arguments.Last();
            }
        }

        public TOut Execute(TIn input)
        {
            throw new NotImplementedException();
        }

        public void RegisterJob<TJobIn, TJobOut>(IPipelineJob<TJobIn, TJobOut> job)
        {
            if (job == null)
                throw new ArgumentNullException("job");

            _jobs.Add(job);
        }
    }
}
