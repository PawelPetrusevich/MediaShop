﻿using MediaShop.Common.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Set permission
        /// </summary>
        /// <param name="permissionDto">Permissions data</param>
        /// <returns>account</returns>
        Account SetPermission(PermissionDto permission);

        /// <summary>
        /// Remove permission
        /// </summary>
        /// <param name="permissionDto">Permissions data</param>
        /// <returns>account</returns>
        Account RemovePermission(PermissionDto permission);
    }
}
