namespace Skyland.Pipeline.Internal.Decorators
{
    internal class JobComponentDecorator<TIn, TOut> : IJobComponent
    {
        private readonly IJobComponent<TIn, TOut> _job;
         
        public JobComponentDecorator(IJobComponent<TIn, TOut> job)
        {
            _job = job;
        }

        public object Execute(object obj)
        {
            return _job.Execute((TIn) obj);
        }
    }
}
