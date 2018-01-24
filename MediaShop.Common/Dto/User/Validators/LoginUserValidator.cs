using System;
using System.Linq;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.User.Validators
{
    public class LoginUserValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly IAccountRepository _repository;

        public LoginUserValidator(IAccountRepository repository)
        {
            this._repository = repository;

            this.RuleFor(m => m.Login).Must(this.CheckExistingUser).WithMessage(model => $"User exists with login {model.Login}");
            this.RuleFor(m => m.Password).NotEmpty().MinimumLength(3).WithMessage(Resources.IncorrectPassword);
        }

        private bool CheckExistingUser(string login)
        {
            return this._repository.GetByLogin(login) != null;
        }
    }
}