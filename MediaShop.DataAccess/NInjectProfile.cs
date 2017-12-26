using MediaShop.Common.Interfaces.Repositories;
using MediaShop.DataAccess.Repositories;
using Ninject.Modules;

namespace MediaShop.DataAccess
{
    public class NInjectProfile : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}
