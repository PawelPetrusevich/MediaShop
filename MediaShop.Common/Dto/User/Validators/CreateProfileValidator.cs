namespace MediaShop.Common.Dto.User.Validators
{
    using FluentValidation;

    public class CreateProfileValidator : AbstractValidator<ProfileDto>
    {
        public CreateProfileValidator()
        {
            this.RuleFor(p => p.Email).NotEmpty().WithMessage("Email can't be empty");
            this.RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("DOB can't be empty");
        }
    }
}