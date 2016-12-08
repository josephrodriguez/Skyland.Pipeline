#region using

using Skyland.Pipeline.Enums;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// Represent the output of pipeline job
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class PipelineResult<TResult>
    {
        private readonly Status _status;

        internal PipelineResult(TResult result)
        {
            Result = result;
            _status = Status.Completed;
        }

        internal PipelineResult(Status status)
        {
            Result = default(TResult);
            _status = status;
        }

        internal Status Status
        {
            get { return _status; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if all registered stages were executed without any error; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted
        {
            get { return _status == Status.Completed; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is faulted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is faulted; otherwise, <c>false</c>.
        /// </value>
        public bool IsFaulted
        {
            get { return _status == Status.Error; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is rejected.
        /// </summary>
        /// <value>
        /// <c>true</c> if any filter of one stage return false; otherwise, <c>false</c>.
        /// </value>
        public bool IsRejected
        {
            get { return _status == Status.Rejected; }
        }

        /// <summary>
        /// Gets the final result value of pipeline job.
        /// </summary>
        public TResult Result { get; private set; }

    }
}
