using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.Messaging.Validators;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Helpers;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture]

    public class RestorePasswordTest
    {
        private AccountService _accountService;

        private Mock<IAccountFactoryRepository> _factoryRepositoryMock;
        private Mock<IEmailService> _emailServiceMock;
        private Mock<IValidator<RegisterUserDto>> _validatorMock;
        private IAccountTokenFactoryValidator _tokenValidator;

        private AccountDbModel _accountDb;
        private ResetPasswordDto _resetPasswordDto;

        public RestorePasswordTest()
        {
            Mapper.Reset();
            Mapper.Initialize(x => { x.AddProfile<MapperProfile>(); });
        }

        [SetUp]

        public void Initialize()
        {
            _factoryRepositoryMock = new Mock<IAccountFactoryRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _validatorMock = new Mock<IValidator<RegisterUserDto>>();

            _validatorMock.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(true);
            _factoryRepositoryMock.Setup(x => x.Accounts.Add(It.IsAny<AccountDbModel>())).Returns(new AccountDbModel());
            _emailServiceMock.Setup(x => x.SendRestorePwdLink(It.IsAny<AccountPwdRestoreDto>()));

            _tokenValidator = new AccountTokenFactoryValidator(new ExtAccountConfirmationValidator(_factoryRepositoryMock.Object.Accounts), new ExtAccountPwdRestoreValidator(_factoryRepositoryMock.Object.Accounts));



            _accountService = new AccountService(_factoryRepositoryMock.Object, _emailServiceMock.Object, this._validatorMock.Object, _tokenValidator);

            _accountDb = new AccountDbModel() { Email = "noreply.mediashop@gmail.com" };

            _resetPasswordDto = new ResetPasswordDto()
            {
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "noreply.mediashop@gmail.com"
            };
        }

        [Test]
        public void InitRecoveryPasswordWrongEmailTest()
        {
            _factoryRepositoryMock.Setup(x => x.Accounts.GetByEmail(It.IsAny<string>())).Returns((AccountDbModel)null);
            var model = new ForgotPasswordDto()
            {
                Email = "test",
                Origin = "test"
            };

            Assert.Throws<NotFoundUserException>(() => _accountService.InitRecoveryPassword(model));
        }

        [Test]
        public void RecoveryPasswordTest()
        {
            string token = TokenHelper.NewToken();
            _resetPasswordDto.Token = token;
            _accountDb.AccountConfirmationToken = token;

            AccountDbModel actualDbModel = new AccountDbModel();
            _factoryRepositoryMock.Setup(x => x.Accounts.GetByEmail(It.IsAny<string>())).Returns(_accountDb);

            _factoryRepositoryMock.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Callback<AccountDbModel>(
                (m) =>
                {
                    actualDbModel = m;

                }).Returns<AccountDbModel>(m => m);

            var actual = _accountService.RecoveryPassword(_resetPasswordDto);

            Assert.AreEqual(_resetPasswordDto.Password.GetHash(), actual.Password);
            Assert.AreNotEqual(actualDbModel.AccountConfirmationToken, token);
        }

        [Test]
        public void RecoveryPasswordExceptionsTest()
        {
            string token = TokenHelper.NewToken();
            _resetPasswordDto.Token = token;
            _accountDb.AccountConfirmationToken = TokenHelper.NewToken();

            _factoryRepositoryMock.Setup(x => x.Accounts.GetByEmail(It.IsAny<string>())).Returns((AccountDbModel)null);
            _factoryRepositoryMock.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns<AccountDbModel>(m => m);

            Assert.Throws<ModelValidateException>(() => _accountService.RecoveryPassword(_resetPasswordDto));

            _factoryRepositoryMock.Setup(x => x.Accounts.GetByEmail(It.IsAny<string>())).Returns(_accountDb);

            Assert.Throws<ModelValidateException>(() => _accountService.RecoveryPassword(_resetPasswordDto));
        }
    }
}
