#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Skyland.Pipeline.Handler;

#endregion

namespace Skyland.Pipeline.Impl
{
    internal class DefaultPipeline<TInput, TOutput> : IPipeline<TInput, TOutput>
    {
        private readonly IList<object> _jobs;

        #region Events

        public event PipelineErrorHandler OnError;

        #endregion

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

                try {
                    current = invokableMethod.Invoke(job, new[] { current });
                }
                catch (Exception exception)
                {
                    //TargetInvocationException is handled here, Base exception must be propagated
                    var e = exception.GetBaseException();

                    if (OnError != null)
                        OnError(job, exception);
                    else
                        throw exception.GetBaseException();
                }
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
