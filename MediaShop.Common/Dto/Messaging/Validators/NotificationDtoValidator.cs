﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class NotificationDtoValidator : AbstractValidator<NotificationDto>
    {
        public NotificationDtoValidator()
        {
            this.RuleFor(n => n).NotNull().WithMessage(Resources.NullOrEmptyValue);
            this.RuleFor(n => n.Message).NotNull().NotEmpty().WithMessage((x, s) => {
                //return s;
                return string.Format(Resources.NullOrEmptyValueString, nameof(x.Message)); });
            this.RuleFor(n => n.Title).NotNull().NotEmpty();
            this.RuleFor(n => n.ReceiverId).GreaterThan(0).WithMessage(Resources.LessThanOrEqualToZeroValue);
            this.RuleFor(n => n.SenderId).GreaterThan(0).WithMessage(Resources.LessThanOrEqualToZeroValue);
        }
    }
}
