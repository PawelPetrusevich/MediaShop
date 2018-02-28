using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Repositories
{
    /// <summary>
    /// Interface IProfileRepository
    /// </summary>
    /// <seealso cref="IRepository{StatisticDbModel}" />
    public interface IStatisticRepository : IRepository<StatisticDbModel>, IRepositoryAsync<StatisticDbModel>
    {
    }
}
