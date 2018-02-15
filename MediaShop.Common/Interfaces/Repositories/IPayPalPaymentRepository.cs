namespace MediaShop.Common.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the repository when working with the Payment class
    /// </summary>
    public interface IPayPalPaymentRepository : IRepository<PayPalPaymentDbModel>, IRepositoryAsync<PayPalPaymentDbModel>
    {
    }
}
