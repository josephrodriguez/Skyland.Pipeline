#region using

using System;
using Skyland.Pipeline.Components.Filters;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline.Containers.Impl
{
    internal class FilterExecutionContainer<TIn> : IFilterExecutionContainer
    {
        private readonly IFilterComponent<TIn> _filter;

        public FilterExecutionContainer(IFilterComponent<TIn> filter)
        {
            _filter = filter;
        }

        public PipelineOutput<object> Execute(object obj, PipelineErrorHandler handler)
        {
            try
            {
                var filtered = _filter.Execute((TIn)obj);

                return
                    filtered
                        ? new PipelineOutput<object>(OutputStatus.Completed)
                        : new PipelineOutput<object>(OutputStatus.Rejected); 
            }
            catch (Exception exception)
            {
                if (handler == null)
                    throw;

                handler(_filter, exception);

                return new PipelineOutput<object>(OutputStatus.Error);
            }
        }
    }
}
