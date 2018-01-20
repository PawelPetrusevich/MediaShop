namespace MediaShop.BusinessLogic.Services
{
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Resources;

    using MediaShop.BusinessLogic.Properties;

    public static class EmailService
    {
        public static void SendConfirmation(string email, long id)
        {
            try
            {
                //ResourceManager rm = new ResourceManager("MediaShop.BusinessLogic.Properties.Resources", Assembly.GetExecutingAssembly());
                MailMessage mail =
                    new MailMessage(new MailAddress(Resources.ResourceManager.GetString("MediaShopMailAddress")), new MailAddress(email))
                        {
                            Subject =
                                "Registration message"
                        };

                mail.IsBodyHtml = true;
                mail.Body = Resources.RegisterUserMailBody;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                                            {
                                                Credentials = new NetworkCredential(
                                                    Resources.ResourceManager.GetString("MediaShopMailAddress"),
                                                    Resources.ResourceManager.GetString("MedaiShopMailPassword")),
                                                EnableSsl = true
                                            };

                smtpClient.Send(mail);
            }
            catch (SmtpException ex)
            {
                if (ex.StatusCode != SmtpStatusCode.Ok)
                {
                    return;
                }
            }
        }
    }
}