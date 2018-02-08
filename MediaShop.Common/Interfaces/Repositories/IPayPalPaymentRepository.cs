namespace MediaShop.Common.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the repository when working with the Payment class
    /// </summary>
    public interface IPayPalPaymentRepository : IRepository<PayPalPaymentDbModel>
    {
        /// <summary>
        /// Async method for add new object typeof Payment in repository
        /// </summary>
        /// <param name="payment">object Payment for save</param>
        /// <returns>task</returns>
        Task<PayPalPaymentDbModel> AddAsync(PayPalPaymentDbModel payment);
    }
}
