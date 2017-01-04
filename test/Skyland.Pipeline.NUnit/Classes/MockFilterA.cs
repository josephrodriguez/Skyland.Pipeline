#region using

using Skyland.Pipeline.Components;
using Skyland.Pipeline.Components.Filters;

#endregion

namespace Skyland.Pipeline.NUnit.Classes
{
    class MockFilterA : IFilterComponent<MockClassA>
    {
        public bool Execute(MockClassA element)
        {
            return element.Field1 > 0;
        }
    }
}
