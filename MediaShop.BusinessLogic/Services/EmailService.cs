namespace MediaShop.BusinessLogic.Services
{
    using System.Net;
    using System.Net.Mail;

    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Interfaces.Services;

    public class EmailService : IEmailService
    {
        public bool SendConfirmation(string email, long id)
        {
            try
            {
                MailMessage mail =
                    new MailMessage(new MailAddress(Resources.MediaShopMailAddress), new MailAddress(email))
                    {
                        Subject = "Registration message",
                        IsBodyHtml = true,
                        Body = Resources.RegisterUserMailBody
                    };

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        Resources.MediaShopMailAddress,
                        Resources.MediaShopMailPassword),
                    EnableSsl = true
                };

                smtpClient.Send(mail);
            }
            catch (SmtpException ex)
            {
                if (ex.StatusCode != SmtpStatusCode.Ok)
                {
                    return false;
                }
            }

            return true;
        }
    }
}