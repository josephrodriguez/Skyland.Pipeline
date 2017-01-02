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
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PipelineException(string message)
            : base(message)
        {
        }
    }
}
