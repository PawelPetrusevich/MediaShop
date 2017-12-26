<<<<<<< HEAD
﻿namespace MediaShop.DataAccess
=======
﻿using MediaShop.DataAccess.Context;
using Ninject.Modules;

namespace MediaShop.DataAccess
>>>>>>> develop
{
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
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
<<<<<<< HEAD
            this.Bind<ICartRespository<ContentCartDto>>().To<CartRepository>();
=======
            Bind<MediaContext>().ToSelf();
>>>>>>> develop
        }
    }
}
