#region using

using System;

#endregion

namespace Skyland.Pipeline.Delegates
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="exception"></param>
    public delegate void ErrorHandler(object sender, Exception exception);
}
