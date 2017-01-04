#region using

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Skyland.Pipeline.Services
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

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">serviceType</exception>
        /// <exception cref="System.Exception"></exception>
        public object GetService(Type serviceType)
        {
            if(serviceType == (Type) null)
                throw new ArgumentNullException(nameof(serviceType));
            if(!_singleInstances.Contains(serviceType))
                throw new Exception();

            return _singleInstances[serviceType];
        }

        /// <summary>
        /// Replaces the single.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="service">The service.</param>
        /// <exception cref="System.ArgumentNullException">serviceType</exception>
        protected void ReplaceSingle(Type serviceType, object service)
        {
            if (serviceType == (Type)null)
                throw new ArgumentNullException(nameof(serviceType));
            _singleInstances[serviceType] = service;
        }

        /// <summary>
        /// Sets the single.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service">The service.</param>
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
