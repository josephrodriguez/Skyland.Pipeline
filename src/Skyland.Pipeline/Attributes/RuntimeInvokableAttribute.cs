#region using

using System;

#endregion

namespace Skyland.Pipeline.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class RuntimeInvokableAttribute : Attribute
    {
    }
}
