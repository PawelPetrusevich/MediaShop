using MediaShop.DataAccess.Context;
using Ninject.Modules;

namespace MediaShop.DataAccess
{
    /// <summary>
    /// Profile Ninject container
    /// </summary>
    public class NInjectProfile : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            Bind<MediaContext>().ToSelf();
        }
    }
}
