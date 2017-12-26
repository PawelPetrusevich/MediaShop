namespace MediaShop.DataAccess
{
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.DataAccess.Context;
    using MediaShop.DataAccess.Repositories;
    using Ninject.Modules;

    public class NInjectProfile : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();

            Bind<MediaContext>().ToSelf();
        }
    }
}
