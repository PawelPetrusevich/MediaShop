﻿using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    public class PermissionDomain
    {
        /// <summary>
        /// Gest or sets role
        /// </summary>
        /// <value>The role</value>
        public Role Role { get; set; } = Role.User;
    }
}