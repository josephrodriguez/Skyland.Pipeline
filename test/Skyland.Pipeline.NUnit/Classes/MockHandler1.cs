#region using

using System;
using System.Diagnostics;
using Skyland.Pipeline.Components;

#endregion

namespace Skyland.Pipeline.NUnit.Classes
{
    class MockHandler1:IHandlerComponent<int>
    {
        public void Handle(int obj)
        {
            Trace.TraceInformation(obj.ToString());
        }
    }
}
