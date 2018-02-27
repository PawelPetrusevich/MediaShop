using FluentValidation;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class AccountPwdRestoreValidator : AbstractValidator<ResetPasswordDto>
    {
        public AccountPwdRestoreValidator()
        {
            this.RuleFor(m => m.Email).NotEmpty().MinimumLength(5).WithMessage(Resources.IncorrectEmail);
            this.RuleFor(m => m.Token).NotEmpty().MinimumLength(5).WithMessage(Resources.IncorrectToken);
            this.RuleFor(m => m.Password).NotEmpty().MinimumLength(5).Equal(m => m.ConfirmPassword).WithMessage(Resources.PasswordDoNotMatch);
        }
    }
}