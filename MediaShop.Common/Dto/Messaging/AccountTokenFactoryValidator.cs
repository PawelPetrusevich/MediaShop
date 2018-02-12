using FluentValidation;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.Common.Dto.Messaging
{
    public class AccountTokenFactoryValidator : IAccountTokenFactoryValidator
    {
        public AccountTokenFactoryValidator(IValidator<AccountConfirmationDto> accountConfirmation, IValidator<ResetPasswordDto> accountPwdRestore)
        {
            AccountConfirmation = accountConfirmation;
            AccountPwdRestore = accountPwdRestore;
        }

        public IValidator<AccountConfirmationDto> AccountConfirmation { get; set; }

        public IValidator<ResetPasswordDto> AccountPwdRestore { get; set; }
    }
}