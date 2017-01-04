#region using

using System.Diagnostics;
using Skyland.Pipeline.Components;
using Skyland.Pipeline.Components.Handlers;

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
