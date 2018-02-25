using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using MediaShop.Common.Dto.Messaging.Validators;

namespace MediaShop.Common.Dto.Messaging
{
    [Validator(typeof(AccountConfirmationValidator))]
    public class AccountConfirmationDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string Origin { get; set; }
    }
}