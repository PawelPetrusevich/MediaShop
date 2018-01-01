using MediaShop.DataAccess.Context;
using Ninject.Modules;

namespace MediaShop.DataAccess
{
    public class NInjectProfile : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();

            Bind<MediaContext>().ToSelf();
        }
    }
}
