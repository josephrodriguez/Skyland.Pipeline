#region using

using System;

#endregion

namespace Skyland.Pipeline.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class PipelineException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineException"/> class.
        /// </summary>
        public PipelineException()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PipelineException(string message)
            : base(message)
        {
        }
    }
}
