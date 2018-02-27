using FluentValidation;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class AccountConfirmationValidator : AbstractValidator<AccountConfirmationDto>
    {
        public AccountConfirmationValidator()
        {
            this.RuleFor(m => m.Email).NotEmpty().MinimumLength(5).WithMessage(Resources.IncorrectEmail);
            this.RuleFor(m => m.Token).NotEmpty().MinimumLength(5).WithMessage(Resources.IncorrectToken);
        }
    }
}