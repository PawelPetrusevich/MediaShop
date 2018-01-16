namespace MediaShop.Common.Interfaces.Repositories
{
    using System.Collections.Generic;

    using MediaShop.Common.Models.User;

    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> GetByAccount(Account account);
    }
}