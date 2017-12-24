namespace MediaShop.BusinessLogic
{
    using MediaShop.BusinessLogic.Services;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using Ninject.Modules;

    public class NInjectProfile : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            this.Bind<ICartService<ContentClassForUnitTest, ContentCart>>().To<CartService>();
        }
    }
}
