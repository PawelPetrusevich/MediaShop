// <copyright file="CollectionExtensions.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for ICollection
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Remove suitable elements
        /// </summary>
        /// <param name="collection">Collection</param>
        /// <param name="predicate">Criteria</param>
        /// <typeparam name="T">Generic type param</typeparam>
        /// <returns>Count of deleted items</returns>
        public static int Remove<T>(this ICollection<T> collection, Predicate<T> predicate)
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
