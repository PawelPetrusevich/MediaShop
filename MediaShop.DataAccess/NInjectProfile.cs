namespace MediaShop.DataAccess
{
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Context;
    using MediaShop.DataAccess.Repositories;
    using Ninject.Modules;

    /// <summary>
    /// Profile Ninject container
    /// </summary>
    public class NInjectProfile : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            this.Bind<ICartRespository<ContentCartDto>>().To<CartRepository>();
            this.Bind<MediaContext>().ToSelf();
        }
    }
}
