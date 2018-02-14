using FluentValidation;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IAccountTokenFactoryValidator
    {
        IValidator<AccountConfirmationDto> AccountConfirmation { get; set; }

        IValidator<ResetPasswordDto> AccountPwdRestore { get; set; }
    }
}