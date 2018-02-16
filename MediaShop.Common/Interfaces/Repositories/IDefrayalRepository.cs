namespace MediaShop.Common.Interfaces.Repositories
{
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Interface IDefrayalRepository
    /// </summary>
    public interface IDefrayalRepository : IRepository<DefrayalDbModel>, IRepositoryAsync<DefrayalDbModel>
    {
    }
}
