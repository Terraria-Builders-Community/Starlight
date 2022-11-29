using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> SelectWhere<T>(this IEnumerable enumerable)
        {
            foreach (var t in enumerable)
            {
                if (t is T @out)
                    yield return @out;
            }
        }

        public static IEnumerable<T> SelectWhere<T>(this IEnumerable enumerable, Func<T, bool> predicate)
        {
            foreach (var t in enumerable)
            {
                if (t is T @out)
                {
                    if (predicate(@out))
                        yield return @out;
                }
            }
        }
    }
}
