using MediaShop.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IAccountFactoryRepository
    {
       IAccountRepository Accounts { get; set; }

       IProfileRepository Profiles { get; set; }

       ISettingsRepository Settings { get; set; }

       IStatisticRepository Statistics { get; set; }
    }
}
