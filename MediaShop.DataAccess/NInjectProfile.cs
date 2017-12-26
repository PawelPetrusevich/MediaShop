using MediaShop.DataAccess.Context;
using Ninject.Modules;

namespace MediaShop.DataAccess
{
    public class NInjectProfile : NinjectModule
    {
        public override void Load()
        {
            Bind<MediaContext>().ToSelf();
        }
    }
}
