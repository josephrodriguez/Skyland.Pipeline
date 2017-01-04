#region using

using System;
using System.Collections;
using System.Collections.Generic;
using Skyland.Pipeline.Delegates;

#endregion

namespace Skyland.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceContainer : IDisposable
    {
        private IDictionary _singleInstances;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContainer"/> class.
        /// </summary>
        public ServiceContainer()
        {
            _singleInstances = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Replaces the specified type.
        /// </summary>
        /// <param name="serviceType">The type.</param>
        /// <param name="service">The instance.</param>
        public void Replace(Type serviceType, object service)
        {
            if(serviceType == (Type) null)
                throw new ArgumentNullException(nameof(serviceType));
            if (service != null && !serviceType.IsInstanceOfType(service))
                throw new ArgumentException("Service instance must derive from specified service type.");
            ReplaceSingle(serviceType, service);
        }

        public object GetService(Type serviceType)
        {
            if(serviceType == (Type) null)
                throw new ArgumentNullException(nameof(serviceType));
            if(!_singleInstances.Contains(serviceType))
                throw new Exception();

            return _singleInstances[serviceType];
        }

        protected void ReplaceSingle(Type serviceType, object service)
        {
            if (serviceType == (Type)null)
                throw new ArgumentNullException(nameof(serviceType));
            _singleInstances[serviceType] = service;
        }

        protected void SetSingle<T>(T service)
        {
            _singleInstances[typeof(T)] = service;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            _singleInstances = null;
        }
    }
}
