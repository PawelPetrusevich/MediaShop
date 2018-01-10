﻿using FluentValidation;

namespace MediaShop.Common.Dto.User.Validators
{
    public class RegisterUserVaildator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserVaildator()
        {
            this.RuleFor(m => m.Login).NotEmpty().MinimumLength(5).WithMessage("Blablabla");
        }
    }
}