using MediaShop.Common.Interfaces.Services;
using Ninject.Modules;

namespace MediaShop.BusinessLogic
{
    public class NInjectProfile : NinjectModule
    {
        public override void Load()
        {
            this.Kernel?.Bind<IUserService>().To<UserService>();
        }
    }
}
