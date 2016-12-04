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
        private readonly IList<object> _stages;

        #region Events

        internal event PipelineErrorHandler OnError;

        #endregion

        public DefaultPipeline() {
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

        public TOutput Execute(TInput input)
        {
            object current = input;

            foreach (var stage in _stages)
            {
                var genericInterface = stage.GetType()
                    .GetInterfaces()
                    .Single(
                        i => i.GetGenericTypeDefinition() == typeof(IPipelineStage<,>));

                var invokableMethod = genericInterface.GetMethods().Single();

                try {
                    current = invokableMethod.Invoke(stage, new[] { current });
                }
                catch (Exception exception) {
                    //TargetInvocationException is handled here, Base exception must be propagated
                    if (OnError != null)
                        OnError(stage, exception.GetBaseException());
                    else
                        throw exception.GetBaseException();
                }
            }

            return (TOutput) current;
        }

        internal void RegisterStage<TIn, TOut>(PipelineStage<TIn, TOut> stage)
        {
            if (stage == null)
                throw new ArgumentNullException("stage");

            _stages.Add(stage);
        }
    }
}
