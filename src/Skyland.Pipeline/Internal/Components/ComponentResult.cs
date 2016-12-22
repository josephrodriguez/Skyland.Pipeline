#region using

using Skyland.Pipeline.Internal.Enums;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal class ComponentResponse
    {
        public static readonly ComponentResponse Rejected = new ComponentResponse(ResponseStatus.Rejected);
        public static readonly ComponentResponse Completed = new ComponentResponse(ResponseStatus.Completed);
        public static readonly ComponentResponse Error = new ComponentResponse(ResponseStatus.Error);

        public object Result { get; private set; }

        public ResponseStatus Status { get; private set; }

        public ComponentResponse(ResponseStatus status)
        {
            Status = status;
            Result = default(object);
        }

        public ComponentResponse(object result)
        {
            Status = ResponseStatus.Completed;
            Result = result;
        }
    }
}
