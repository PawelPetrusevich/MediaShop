using System;
using System.Linq;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;

namespace MediaShop.Common.Dto.User.Validators
{
    public class ExistingUserValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly IAccountRepository _repository;

        public ExistingUserValidator(IAccountRepository repository)
        {
            this._repository = repository;

            this.RuleFor(m => m.Login).Must(this.CheckExistingLogin)
                .WithMessage(model => $"User exist with login {model.Login}");
            this.RuleFor(m => m.Email).Must(this.CheckExistingUser)
                .WithMessage(model => $"User exists with email {model.Email}");
            this.RuleFor(m => m.Password).Equal(m => m.ConfirmPassword).WithMessage("Passwords must match");
            this.RuleFor(m => m.Email).EmailAddress();
        }

        private bool CheckExistingUser(string email)
        {
            return !this._repository.Find(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).Any();
        }

        private bool CheckExistingLogin(string login)
        {
            return !this._repository.Find(m => m.Login.Equals(login, StringComparison.OrdinalIgnoreCase)).Any();
        }
    }
}