using System.Security.Cryptography.X509Certificates;
using Skyland.Pipeline.Handler;

namespace Skyland.Pipeline
{
    public interface IPipeline<in TIn, out TOut>
    {
        #region Methods

        TOut Execute(TIn input);

        #endregion

        #region Events

        event PipelineErrorHandler OnError;

        #endregion
    }
}
