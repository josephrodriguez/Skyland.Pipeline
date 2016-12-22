namespace Skyland.Pipeline.Internal.Decorators
{
    internal class FilterComponentDecorator<TIn> : IFilterComponent
    {
        private readonly IFilterComponent<TIn> _filter;

        public FilterComponentDecorator(IFilterComponent<TIn> filter)
        {
            _filter = filter;
        }

        public bool Execute(object obj)
        {
            return _filter.Execute((TIn) obj);
        }
    }
}
