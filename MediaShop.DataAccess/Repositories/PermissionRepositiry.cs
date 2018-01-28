namespace MediaShop.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Repositories.Base;

    public class PermissionRepository : Repository<PermissionDbModel>, IPermissionRepository
    {
        public PermissionRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<PermissionDbModel> GetByAccount(AccountDbModel accountDbModel)
        {
            return this.DbSet.Where(p => p.AccountDbModel == accountDbModel);
        }
    }
}