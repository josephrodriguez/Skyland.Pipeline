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
        public static IEnumerable<PropertyInfo> GetProperties(this object obj, Type type, BindingFlags flags)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return
                obj.GetType()
                .GetProperties(flags)
                .Where(
                    prop => prop.PropertyType == type);
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>(this object obj, BindingFlags flags)
        {
            return GetProperties(obj, typeof(T), flags);
        }

        public static IEnumerable<T> GetPropertyValues<T>(this object obj, BindingFlags flags)
        {
            return
                obj.GetProperties<T>(flags)
                    .Select(property => (T) property.GetValue(obj, null));
        }

        public static IEnumerable<object> GetPropertyValues(this object obj, Type type, BindingFlags flags)
        {
            return
                obj.GetProperties(type, flags)
                    .Select(property => property.GetValue(obj, null));
        }
    }
}
