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

    using FluentValidation;

    using MediaShop.BusinessLogic.Services;
    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.User;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Class UserServiceRemoveRoleTests.
    /// </summary>
    [TestFixture]
    public class UserServiceRemoveRoleTests
    {
        /// <summary>
        /// Tests the method remove role is true.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="role">The role.</param>
        [TestCase(5l, Role.User)]
        [TestCase(5l, Role.Admin)]

        public void TestMethodRemoveRoleIsTrue(int role)
        {
            var storage = new Mock<IAccountRepository>();
            var storagePermission = new Mock<IPermissionRepository>();
            var storageEmailService = new Mock<IEmailService>();
            var validator = new Mock<AbstractValidator<RegisterUserDto>>();
        var permissions = new List<PermissionDbModel>
                                  {
                                      new PermissionDbModel() { Role = Role.Admin },
                                      new PermissionDbModel() { Role = Role.User }
                                  };
            var profile = new ProfileDbModel { Id = 1 };
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
            var userService = new AccountService(storage.Object, storagePermission.Object, storageEmailService.Object,validator.Object);
            Assert.IsTrue(userService.RemoveRole(roleUserBl));
        }

        /// <summary>
        /// Tests the method remove role is false.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="role">The role.</param>
        [TestCase(5ul, Role.User)]

        public void TestMethodRemoveRoleIsFalse(int role)
        {
            var storage = new Mock<IAccountRepository>();
            var storagePermission = new Mock<IPermissionRepository>();
            var storageEmailService = new Mock<IEmailService>();
            var validator = new Mock<AbstractValidator<RegisterUserDto>>();

            var permissions = new List<PermissionDbModel>{new PermissionDbModel() { Role = Role.Admin }};
            var profile = new ProfileDbModel { Id = 1 };
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
            var userService = new AccountService(storage.Object, storagePermission.Object, storageEmailService.Object,validator.Object);
            Assert.IsFalse(userService.RemoveRole(roleUserBl));
        }
    }
}
