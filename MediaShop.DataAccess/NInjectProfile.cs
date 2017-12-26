<<<<<<< HEAD
﻿using MediaShop.Common.Interfaces.Repositories;
using MediaShop.DataAccess.Repositories;
=======
﻿using MediaShop.DataAccess.Context;
>>>>>>> develop
using Ninject.Modules;

namespace MediaShop.DataAccess
{
    public class NInjectProfile : NinjectModule
    {
        public override void Load()
        {
<<<<<<< HEAD
            Bind<IProductRepository>().To<ProductRepository>();
=======
            Bind<MediaContext>().ToSelf();
>>>>>>> develop
        }
    }
}
