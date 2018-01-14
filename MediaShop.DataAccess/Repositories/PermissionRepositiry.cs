namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;

    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Repositories.Base;

    public class PermissionRepositiry : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepositiry(DbContext context)
            : base(context)
        {
        }
    }
}