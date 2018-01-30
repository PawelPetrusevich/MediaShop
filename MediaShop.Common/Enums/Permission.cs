// <copyright file="Permission.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models.User
{
    using System;

    /// <summary>
    /// Permissions value
    /// </summary>
    [Flags]
    public enum Permission : int
    {
        /// <summary>
        /// View
        /// has mask 0001
        /// </summary>
        See = 1,

        /// <summary>
        /// Create(add)
        /// has mask 0010
        /// </summary>
        Create = 2,

        /// <summary>
        /// Delete
        /// has mask 0100
        /// </summary>
        Delete = 4
    }
}
