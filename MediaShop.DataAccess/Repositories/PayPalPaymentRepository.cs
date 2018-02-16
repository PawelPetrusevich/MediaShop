namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.PaymentModel;
    using MediaShop.DataAccess.Context;
    using MediaShop.DataAccess.Repositories.Base;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class PayPalPaymentRepository : Repository<PayPalPaymentDbModel>, IPayPalPaymentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayPalPaymentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PayPalPaymentRepository(MediaContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Method for add object type Payment in repository
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public override PayPalPaymentDbModel Add(PayPalPaymentDbModel model)
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
        public override async Task<PayPalPaymentDbModel> AddAsync(PayPalPaymentDbModel model)
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
