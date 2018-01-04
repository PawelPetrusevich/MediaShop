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
        /// <summary>
        /// Tests the method remove role is true.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="role">The role.</param>
        [TestCase(5ul, Role.User)]
        [TestCase(5ul, Role.Admin)]

        public void TestMethodRemoveRoleIsTrue(ulong n, Role role)
        {
            var storage = new Mock<IAccountRepository>();

            var listRoles = new SortedSet<Role> { Role.Admin, Role.User };
            var profile = new Profile { Id = n };
            var user = new Account { Login = "User", Password = "123", Profile = profile, Permissions = listRoles };

            var list = new List<Account> { user };
            storage.Setup(s => s.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(list);
            var userService = new UserService(storage.Object);
            Assert.IsTrue(userService.RemoveRole(n, role));
        }

        /// <summary>
        /// Tests the method remove role is false.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="role">The role.</param>
        [TestCase(5ul, Role.User)]

        public void TestMethodRemoveRoleIsFalse(ulong n, Role role)
        {
            var storage = new Mock<IAccountRepository>();

            var listRoles = new SortedSet<Role> { Role.Admin };
            var profile = new Profile { Id = n };
            var user = new Account { Login = "User", Password = "123", Profile = profile, Permissions = listRoles };

            var list = new List<Account> { user };
            storage.Setup(s => s.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(list);
            var userService = new UserService(storage.Object);
            Assert.IsFalse(userService.RemoveRole(n, role));
        }
    }
}
