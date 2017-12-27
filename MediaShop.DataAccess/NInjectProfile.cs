using MediaShop.DataAccess.Context;
using Ninject.Modules;

namespace MediaShop.DataAccess
{
    /// <summary>
    /// the introduction of dependencies
    /// </summary>
    public class NInjectProfile : NinjectModule
    {
        /// <summary>
        /// Load
        /// </summary>
        public override void Load()
        {
            Bind<MediaContext>().ToSelf();
        }
    }
}
