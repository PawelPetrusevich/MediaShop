using System;
using System.Linq;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class ExtAccountPwdRestoreValidator : AccountPwdRestoreValidator
    {
        private IAccountRepository _repository;

        public ExtAccountPwdRestoreValidator(IAccountRepository repository) : base()
        {
            this._repository = repository;

            this.RuleFor(m => m.Email).Must(this.CheckExistingUser)
                .WithMessage(model => Resources.UserNotFound).DependentRules(r => r
                    .RuleFor(m => m.Token).Must((model, token) => this.CheckValidToken(model.Email, token))
                    .WithMessage(model => Resources.IncorrectToken));
        }

        private bool CheckExistingUser(string email)
        {
            return this._repository.GetByEmail(email) != null;
        }

        private bool CheckValidToken(string email, string token)
        {
            var user = this._repository.GetByEmail(email);
            return user.AccountConfirmationToken.Equals(token, StringComparison.OrdinalIgnoreCase);
        }
    }
}