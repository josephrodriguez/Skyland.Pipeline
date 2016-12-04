#region using

using System;
using Skyland.Pipeline.Impl;

#endregion

namespace Skyland.Pipeline
{
    public static class Component
    {
        public static StageComponent<TInput, TOutput> ForJob<TInput, TOutput>(IPipelineJob<TInput, TOutput> job)
        {
            if(job == null)
                throw new ArgumentNullException("job");

            return new StageComponent<TInput, TOutput>(job);
        }

        public static StageComponent<TInput, TOutput> ForJob<TInput, TOutput>(Func<TInput, TOutput> function)
        {
            if(function == null)
                throw new ArgumentNullException("function");

            return ForJob(new InlineJob<TInput, TOutput>(function));
        }
    }
}
