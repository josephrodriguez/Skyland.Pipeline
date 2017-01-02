using System;
using Skyland.Pipeline.Components;
using Skyland.Pipeline.Delegates;
using Skyland.Pipeline.Internal.Enums;

namespace Skyland.Pipeline.Internal.Containers
{
    internal class FilterExecutionContainer<TIn> : IFilterExecutionContainer
    {
        private readonly IFilterComponent<TIn> _filter;

        public FilterExecutionContainer(IFilterComponent<TIn> filter)
        {
            _filter = filter;
        }

        public PipelineOutput<object> Execute(object obj, ComponentErrorHandler handler)
        {
            try
            {
                var output = _filter.Execute((TIn)obj);
                return new PipelineOutput<object>(output);
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
