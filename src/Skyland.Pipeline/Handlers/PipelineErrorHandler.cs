#region using

using System;

#endregion

namespace Skyland.Pipeline.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="exception">The exception.</param>
    public delegate void PipelineErrorHandler(object sender, Exception exception);
}
