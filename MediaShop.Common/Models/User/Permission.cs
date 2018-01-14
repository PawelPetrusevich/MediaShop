// <copyright file="Permission.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models.User
{
    /// <summary>
    /// Class Permission
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    public class Permission : Entity
    {
        /// <summary>
        /// Gest or sets role
        /// </summary>
        /// <value>The role</value>
        public Role Role { get; set; } = Role.User;

        /// <summary>
        /// Gets or sets Account.
        /// </summary>
        /// <value>The account.</value>
        public virtual Account Account { get; set; }
    }
}