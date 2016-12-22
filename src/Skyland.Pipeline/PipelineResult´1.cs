#region using



#endregion

#region using

using Skyland.Pipeline.Internal.Enums;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// Represent the output of pipeline job
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class PipelineResult<TResult>
    {
        private readonly ResponseStatus _status;

        internal PipelineResult(TResult result)
        {
            Result = result;
            _status = ResponseStatus.Completed;
        }

        internal PipelineResult(ResponseStatus status)
        {
            Result = default(TResult);
            _status = status;
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if all registered stages were executed without any error; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted
        {
            get { return _status == ResponseStatus.Completed; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is faulted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is faulted; otherwise, <c>false</c>.
        /// </value>
        public bool IsFaulted
        {
            get { return _status == ResponseStatus.Error; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is rejected.
        /// </summary>
        /// <value>
        /// <c>true</c> if any filter of one stage return false; otherwise, <c>false</c>.
        /// </value>
        public bool IsRejected
        {
            get { return _status == ResponseStatus.Rejected; }
        }

        /// <summary>
        /// Gets the final result value of pipeline job.
        /// </summary>
        public TResult Result { get; private set; }
    }
}
