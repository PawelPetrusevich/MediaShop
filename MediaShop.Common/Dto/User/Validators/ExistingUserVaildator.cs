using System;
using System.Linq;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;

namespace MediaShop.Common.Dto.User.Validators
{
    public class ExistingUserVaildator : AbstractValidator<RegisterUserDto>
    {
        private readonly IAccountRepository _repository;

        public ExistingUserVaildator(IAccountRepository repository)
        {
            _repository = repository;
        }

        public ExistingUserVaildator()
        {
            this.RuleFor(m => m.Email).Must(CheckExistingUser).WithMessage((model) => $"User exists with email {model.Email}");
        }

        private bool CheckExistingUser(string email)
        {
            return !_repository.Find(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).Any();
        }
    }
}