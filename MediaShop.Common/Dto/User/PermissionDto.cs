﻿using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    public class PermissionDto
    {
        public long Id { get; set; }

        public Permission Permission { get; set; }
    }
}