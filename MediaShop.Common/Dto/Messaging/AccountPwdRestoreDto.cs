using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Dto.Messaging
{
    public class AccountPwdRestoreDto
    {
        public string Token { get; set; }

        public string Email { get; set; }
    }
}