using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreManager.Extentions
{
    public static class LinqExtention
    {
        public static IEnumerable<TSource> WhereIf<TSource, TItem>(this IEnumerable<TSource> source, bool condition1, bool condition2, Func<TSource, bool> predicate )
        {
            if(condition1)
                if(condition2)
                return source.Where(predicate);
           

            return source;
        }
    }
}
