using System;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class ExtAccountConfirmationValidator : AccountConfirmationValidator
    {
        private readonly IAccountRepository _repository;

        public ExtAccountConfirmationValidator(IAccountRepository repository) : base()
        {
            this._repository = repository;

            this.RuleFor(m => m.Email).Must(this.CheckExistingUser)
                .WithMessage(model => Resources.UserNotFound)
                .DependentRules(r => r.RuleFor(m => m.Email).Must(this.CheckConfirmedUser).WithMessage(Resources.ConfirmedUser).DependentRules(
                    rr => rr.RuleFor(m => m.Token).Must((model, token) => this.CheckValidToken(model.Email, token))
                        .WithMessage(model => Resources.IncorrectToken)));
        }

        private bool CheckExistingUser(string email)
        {
            return this._repository.GetByEmail(email) != null;
        }

        private bool CheckConfirmedUser(string email)
        {
            return !this._repository.GetByEmail(email).IsConfirmed;
        }

        private bool CheckValidToken(string email, string token)
        {
            var user = this._repository.GetByEmail(email);
            return user.AccountConfirmationToken.Equals(token, StringComparison.OrdinalIgnoreCase);
        }
    }
}