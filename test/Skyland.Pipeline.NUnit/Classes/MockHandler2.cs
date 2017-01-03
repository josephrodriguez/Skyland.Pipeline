﻿#region using

using System.Diagnostics;
using Skyland.Pipeline.Components;

#endregion

namespace Skyland.Pipeline.NUnit.Classes
{
    class MockHandler2 : IHandlerComponent<MockClassA>
    {
        public void Handle(MockClassA obj)
        {
            Trace.TraceInformation("Handled mock class.");
        }
    }
}
