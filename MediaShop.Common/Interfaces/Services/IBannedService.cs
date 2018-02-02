using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto.User;

    public interface IBannedService
    {
        Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag);
    }
}
