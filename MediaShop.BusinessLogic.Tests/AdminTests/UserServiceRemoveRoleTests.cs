using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    using MediaShop.BusinessLogic.Services;

    [TestFixture]
    public class UserServiceRemoveRoleTests
    {
        [TestCase(5, Role.User)]
        [TestCase(5, Role.Admin)]

        public void TestMethodRemoveRoleIsTrue(long n, Role role)
        {
            var storage = new Mock<IRepository<Account>>();

            var listRoles = new SortedSet<Role> {Role.Admin, Role.User};
            var profile = new AccountProfile {Id = n};
            var user = new Account
            {
                Login = "User",
                Password = "123",
                Profile = profile,
                Permissions = listRoles
            };

            var list = new List<Account> {user};
            storage.Setup(s => s.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(list);
            var userService = new UserService(storage.Object);
            Assert.IsTrue(userService.RemoveRole(n, role));
        }

        [TestCase(5, Role.User)]

        public void TestMethodRemoveRoleIsFalse(long n, Role role)
        {
            var storage = new Mock<IRepository<Account>>();

            var listRoles = new SortedSet<Role> {Role.Admin};
            var profile = new AccountProfile { Id = n };
            var user = new Account
            {
                Login = "User",
                Password = "123",
                Profile = profile,
                Permissions = listRoles
            };

            var list = new List<Account> { user };
            storage.Setup(s => s.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(list);
            var userService = new UserService(storage.Object);
            Assert.IsFalse(userService.RemoveRole(n, role));
        }
    }
}
