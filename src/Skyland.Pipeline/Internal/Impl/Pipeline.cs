#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Skyland.Pipeline.Enums;
using Skyland.Pipeline.Handlers;
using Skyland.Pipeline.Internal.Interfaces;
using Skyland.Pipeline.Internal.Reflection;

#endregion

namespace Skyland.Pipeline.Internal.Impl
{
    internal class Pipeline<TInput, TOutput> : IPipeline<TInput, TOutput>
    {
        private readonly IList<object> _stages;

        #region Events

        internal event PipelineErrorHandler OnError;

        #endregion

        public Pipeline() {
            _stages = new List<object>();
        }

        public int Count {
            get { return _stages.Count; } 
        }

        public Type OutputType
        {
            get
            {
                var stage = _stages.LastOrDefault();

                return stage?.GetType().GetGenericArguments().Last();
            }
        }

        public PipelineResult<TOutput> Execute(TInput input)
        {
            object current = input;

            foreach (var stage in _stages)
            {
                var @interface = stage.GetType()
                    .GetInterfaces()
                    .Single(
                        i => 
                            i.GetGenericTypeDefinition() == typeof(IPipelineStage<,>));

                //Get Execute MethodInfo
                var methodInfo = @interface.GetMethods().Single();

                try {
                    var result = methodInfo.Invoke(stage, new[] { current });

                    //Get status of result
                    var status = result.GetPropertyValues<Status>().First();
                    if(status != Status.Completed)
                        return new PipelineResult<TOutput>(status);

                    //Update current output
                    var arguments = @interface.GetGenericArguments();
                    if(arguments.Length != 2)
                        throw new InvalidOperationException();

                    current = result.GetPropertyValues(arguments[1]).First();
                }
                catch (Exception exception)
                {
                    //TargetInvocationException is handled here, Base exception must be propagated
                    if (OnError == null)
                        throw exception.GetBaseException();

                    OnError(stage, exception.GetBaseException());

                    return 
                        new PipelineResult<TOutput>(Status.Error);
                }
            }

            return new PipelineResult<TOutput>((TOutput) current, Status.Completed);
        }

        internal void RegisterStage<TIn, TOut>(PipelineStage<TIn, TOut> stage)
        {
            if (stage == null)
                throw new ArgumentNullException("stage");

            _stages.Add(stage);
        }
    }
}
