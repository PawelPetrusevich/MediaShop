using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaShop.Common.Helpers
{
    public static class ICollectionExtensions
    {
        public static int Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            int deletedCount = 0;

            for (int i = 0; i < collection.Count; i++)
            {
                var element = collection.ElementAt(i);
                if (predicate(element))
                {
                    collection.Remove(element);
                    i--;
                    deletedCount++;
                }
            }

            return deletedCount;
        }
    }
}
