// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserServiceRemoveRoleTests.cs" company="MediaShop">
//   MediaShop
// </copyright>
// <summary>
//   Defines the UserServiceRemoveRoleTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using MediaShop.BusinessLogic.Services;
    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Class UserServiceRemoveRoleTests.
    /// </summary>
    [TestFixture]
    public class UserServiceRemoveRoleTests
    {
        [TestCase(0)]
        [TestCase(1)]

        public void TestMethodRemoveRoleIsTrue(int role)
        {
            var storage = new Mock<IAccountRepository>();
            var storagePermission = new Mock<IPermissionRepository>();
            var permissions = new List<Permission>
                                  {
                                      new Permission() { Role = Role.Admin },
                                      new Permission() { Role = Role.User }
                                  };
            var profile = new Profile { Id = 1 };
            var user = new AccountDbModel
                           {
                               Id = 1,
                               Login = "User",
                               Password = "123",
                               Profile = profile,
                           };
            var roleUserBl = new RoleUserBl { Login = "User", Role = role };
            storage.Setup(s => s.GetByLogin(It.IsAny<string>())).Returns(user);

            storagePermission.Setup(s => s.GetByAccount(It.IsAny<AccountDbModel>())).Returns(permissions);
            var userService = new AccountService(storage.Object, storagePermission.Object);
            Assert.IsTrue(userService.RemoveRole(roleUserBl));
        }

        /// <summary>
        /// Tests the method remove role is false.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="role">The role.</param>
        [TestCase(1)]

        public void TestMethodRemoveRoleIsFalse(int role)
        {
            var storage = new Mock<IAccountRepository>();
            var storagePermission = new Mock<IPermissionRepository>();
            var permissions = new List<Permission>{new Permission() { Role = Role.Admin }};
            var profile = new Profile { Id = 1 };
            var user = new AccountDbModel
                           {
                               Id = 1,
                               Login = "User",
                               Password = "123",
                               Profile = profile,
                           };
            var roleUserBl = new RoleUserBl { Login = "User", Role = role };
            storage.Setup(s => s.GetByLogin(It.IsAny<string>())).Returns(user);

            storagePermission.Setup(s => s.GetByAccount(It.IsAny<AccountDbModel>())).Returns(permissions);
            var userService = new AccountService(storage.Object, storagePermission.Object);
            Assert.IsFalse(userService.RemoveRole(roleUserBl));
        }
    }
}
