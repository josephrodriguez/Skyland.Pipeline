#region using

using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline
{
    public class PipelineResult<TResult>
    {
        public PipelineResult(TResult result, Status status = Status.Completed)
        {
            Result = result;
            Status = status;
        }

        public PipelineResult(Status status)
        {
            Result = default(TResult);
            Status = status;
        }

        public TResult Result { get; private set; }

        public Status Status { get; private set; }
    }
}
