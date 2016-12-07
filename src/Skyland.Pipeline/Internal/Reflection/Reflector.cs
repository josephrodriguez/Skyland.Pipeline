#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace Skyland.Pipeline.Internal.Reflection
{
    internal static class Reflector
    {
        public static IEnumerable<PropertyInfo> GetProperties(this object obj, Type type)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return
                obj.GetType()
                .GetProperties()
                .Where(
                    prop => prop.PropertyType == type);
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>(this object obj)
        {
            return GetProperties(obj, typeof(T));
        }

        public static IEnumerable<T> GetPropertyValues<T>(this object obj)
        {
            return
                obj.GetProperties<T>()
                    .Select(property => (T) property.GetValue(obj, null));
        }

        public static IEnumerable<object> GetPropertyValues(this object obj, Type type)
        {
            return
                obj.GetProperties(type)
                    .Select(property => property.GetValue(obj, null));
        }
    }
}
