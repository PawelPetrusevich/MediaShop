namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Web;

    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Interfaces.Services;

    public class EmailService : IEmailService
    {
        public bool SendConfirmation(string email, long id)
        {
            string encodedEmail = HttpUtility.UrlEncode(email);

            UriBuilder uriBuilder = new UriBuilder
                { Scheme = "http", Port = 51289, Host = "localhost", Path = $"api/account/confirm/{encodedEmail}/{id}" };

            try
             {
                 MailMessage mail =
                     new MailMessage(new MailAddress(Resources.MediaShopMailAddress), new MailAddress(email))
                     {
                         Subject = "Registration message",
                         IsBodyHtml = true,
                         Body = string.Format(Resources.RegisterUserMailBody, uriBuilder.Uri)
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