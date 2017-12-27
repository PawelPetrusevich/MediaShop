using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;


namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IRespository<Account>> _store;
        private UserDto _user;

        [SetUp]
        public void Init()
        {
            var mockRepository =  new Mock<IRespository<Account>>();
            _store = mockRepository;

            _user = new UserDto
            {
                Login = "User",
                Password = "12345",
                UserRole = Role.User
            };
        }

        [Test]
        public void TestRegistrationSuccessfull()
        {
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(new Account());
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>());

            var userService = new UserService(_store.Object);           

            Assert.IsTrue(userService.Register(_user));
        }

        [Test]
        public void TestExistingLogin()
        {
            var permissions = new SortedSet<Role> { Role.User };
            var profile = new AccountProfile { Id = 1 };
            var account = new Account
            {
                Login = "User",
                Password = "12345",
                Profile = profile,
                Permissions = permissions
            };
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(new Account());
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>{account});

            var userService = new UserService(_store.Object);
            Assert.Throws<ExistingLoginException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFail()
        {
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns((Account)null);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>());

            var userService = new UserService(_store.Object);

            Assert.IsFalse(userService.Register(_user));
        }
    }
}
