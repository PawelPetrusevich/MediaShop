using System;
using System.Linq;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.User.Validators
{
    public class ExistingUserValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly IAccountRepository _repository;

        public ExistingUserValidator(IAccountRepository repository)
        {
            this._repository = repository;

            this.RuleFor(m => m.Login).Must(this.CheckExistingLogin)
                .WithMessage(model => Resources.LoginExists);
            this.RuleFor(m => m.Email).Must(this.CheckExistingUser)
                .WithMessage(model => Resources.EmailExists);
            this.RuleFor(m => m.Password).Equal(m => m.ConfirmPassword).WithMessage(Resources.PasswordNotMatch);
            this.RuleFor(m => m.Email).EmailAddress();
        }

        private bool CheckExistingUser(string email)
        {
            var user = this._repository.Find(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            return user == null || user.IsDeleted;
        }

        private bool CheckExistingLogin(string login)
        {
            var user = this._repository.Find(m => m.Login.Equals(login, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            return user == null || user.IsDeleted;
        }
    }
}