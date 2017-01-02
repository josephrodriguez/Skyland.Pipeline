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
    public class PipelineOutput<TResult>
    {
        private readonly OutputStatus _status;

        internal PipelineOutput(TResult result)
        {
            Result = result;
            _status = OutputStatus.Completed;
        }

        internal PipelineOutput(OutputStatus status)
        {
            Result = default(TResult);
            _status = status;
        }

        internal OutputStatus Status
        {
            get { return _status;}
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if all registered stages were executed without any error; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted
        {
            get { return _status == OutputStatus.Completed; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is faulted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is faulted; otherwise, <c>false</c>.
        /// </value>
        public bool IsFaulted
        {
            get { return _status == OutputStatus.Error; }
        }

        /// <summary>
        /// Gets a value indicating whether this pipeline result is rejected.
        /// </summary>
        /// <value>
        /// <c>true</c> if any filter of one stage return false; otherwise, <c>false</c>.
        /// </value>
        public bool IsRejected
        {
            get { return _status == OutputStatus.Rejected; }
        }

        /// <summary>
        /// Gets the final result value of pipeline job.
        /// </summary>
        public TResult Result { get; private set; }
    }
}
