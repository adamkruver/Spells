using System;
using System.Collections.Generic;

namespace Sources.Units.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (action == null) 
                throw new ArgumentNullException(nameof(action));

            foreach (var item in enumerable) 
                action.Invoke(item);

            return enumerable;
        } 
    }
}