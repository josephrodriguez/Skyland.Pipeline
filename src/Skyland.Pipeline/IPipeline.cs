using System.Security.Cryptography.X509Certificates;
using Skyland.Pipeline.Handler;

namespace Skyland.Pipeline
{
    public interface IPipeline<in TIn, out TOut>
    {
        #region Methods

        /// <summary>
        /// Execute in current thread the parameter on inline jobs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TOut Execute(TIn input);

        #endregion
    }
}
