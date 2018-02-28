namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Threading.Tasks;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.PaymentModel;
    using MediaShop.DataAccess.Context;
    using MediaShop.DataAccess.Repositories.Base;

    /// <summary>
    /// Class DefrayalRepository
    /// </summary>
    public class DefrayalRepository : Repository<DefrayalDbModel>, IDefrayalRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefrayalRepository"/> class.
        /// </summary>
        /// <param name="context">context</param>
        public DefrayalRepository(MediaContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Method for add object type Defrayal
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public override DefrayalDbModel Add(DefrayalDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = this.DbSet.Add(model);
            this.Context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Override async add model to repository
        /// </summary>
        /// <param name="model">model user</param>
        /// <returns>db entry</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public override async Task<DefrayalDbModel> AddAsync(DefrayalDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = this.DbSet.Add(model);
            await this.Context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }
    }
}
