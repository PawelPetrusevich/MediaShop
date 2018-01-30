// <copyright file="Role.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models.User
{
    using System;

    /// <summary>
    /// User role
    /// </summary>
    [Flags]
    public enum Role : byte
    {
        /// <summary>
        /// simple role
        /// has mask 0001
        /// </summary>
        User = 1,

        /// <summary>
        /// Customer role
        /// has mask 0010
        /// </summary>
        Customer = 2,

        /// <summary>
        /// Seller role
        /// has mask 0100
        /// </summary>
        Seller = 4,

        /// <summary>
        /// Administrator role
        /// has mask 1000
        /// </summary>
        Admin = 8
    }
}
