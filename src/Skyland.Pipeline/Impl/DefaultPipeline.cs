#region using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Skyland.Pipeline.Impl
{
    internal class DefaultPipeline<TInput, TOutput> : IPipeline<TInput, TOutput>
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

        public TOutput Execute(TInput input)
        {
            object current = input;

            foreach (var job in _jobs)
            {
                var jobType = job.GetType();

                var jobInterface = jobType
                    .GetInterfaces()
                    .Single(i => i.GetGenericTypeDefinition() == typeof(IPipelineJob<,>));

                var invokableMethod = jobInterface.GetMethods().FirstOrDefault();
                if (invokableMethod == null)
                    throw new MissingMethodException("Current job don´t contain expected method implementation.");

                current = invokableMethod.Invoke(job, new[] { current });
            }

            return (TOutput) current;
        }

        internal void RegisterJob<TJobIn, TJobOut>(IPipelineJob<TJobIn, TJobOut> job)
        {
            if (job == null)
                throw new ArgumentNullException("job");

            _jobs.Add(job);
        }
    }
}
