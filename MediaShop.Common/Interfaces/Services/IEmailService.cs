namespace MediaShop.Common.Interfaces.Services
{
    public interface IEmailService
    {
        bool SendConfirmation(string email, long id);
    }
}