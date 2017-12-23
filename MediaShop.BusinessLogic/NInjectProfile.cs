namespace MediaShop.BusinessLogic
{
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using Ninject.Modules;

    public class NInjectProfile : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            this.Bind<ICartService<Entity, ContentCart>>().To<CartService>();
        }
    }
}
