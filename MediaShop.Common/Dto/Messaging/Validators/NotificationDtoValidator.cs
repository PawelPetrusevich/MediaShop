using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class NotificationDtoValidator : AbstractValidator<NotificationDto>
    {
        public NotificationDtoValidator()
        {
            this.RuleFor(n => n.Message).NotEmpty().MinimumLength(5);
            this.RuleFor(n => n.Title).NotEmpty().MinimumLength(5);
            this.RuleFor(n => n.ReceiverId).GreaterThan(0);
            this.RuleFor(n => n.SenderId).GreaterThan(0);
        }
    }
}
