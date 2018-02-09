using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Exceptions.NotificationExceptions;
using MediaShop.Common.Interfaces;

namespace MediaShop.BusinessLogic.Services
{
    using MailKit;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Interfaces.Services;
    using MimeKit;
    using System;
    using System.Configuration;
    using System.Net;
    using System.Web;

    /// <summary>
    /// Email sender service
    /// </summary>
    public class EmailService : IEmailService
    {
        private const byte SendEmailTryCount = 5;

        private SmtpClient _smtpClient;
        private bool _disposed;
        private IEmailSettingsConfig _config;

        public EmailService(IMailService client, IEmailSettingsConfig config)
        {
            _config = config;
            _smtpClient = client as SmtpClient;

            ConnectClient();
            AutenticateClient();
        }

        ~EmailService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Method for send account confirmation
        /// </summary>
        /// <exception cref="EmailTemplatePathException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CountOfTryToEmailSendException"></exception>
        /// <param name="model">Confirmation model</param>
        public void SendConfirmation(AccountConfirmationDto model)
        {
            string templatePath;
            var temaplateName = "AccountConfirmationEmailTemplate";
            if (!_config.TempaltesLocations.TryGetValue(temaplateName, out templatePath))
            {
                throw new EmailTemplatePathException(string.Format(Resources.UnspecifiedPathToTheMessageTemplate, nameof(_config.TempaltesLocations), temaplateName));
            }

            var htmlBody = string.Empty;

            StreamReader sourceReader = null;
            try
            {
                sourceReader = File.OpenText(templatePath);
                htmlBody = sourceReader.ReadToEnd();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(string.Format(Resources.TemplateFileNotFoundExceptionMessage, temaplateName));
            }
            finally
            {
                sourceReader?.Close();
                sourceReader?.Dispose();
            }

            htmlBody = string.Format(htmlBody, _config.WebApiUri.Uri, HttpUtility.UrlEncode(model.Email), model.ConfirmationCode);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Media shop", ((NetworkCredential)_config.Credentials).UserName));
            message.To.Add(new MailboxAddress(model.Email));
            message.Subject = "Email confirmation";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;

            message.Body = builder.ToMessageBody();

            SendEmail(message);
        }

        /// <summary>
        /// Method for send restore link
        /// </summary>
        /// <exception cref="EmailTemplatePathException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CountOfTryToEmailSendException"></exception>
        /// <param name="model"></param>
        public void SendRestorePwdLink(AccountPwdRestoreDto model)
        {
            string templatePath;
            var temaplateName = "AccountPwdRestoreEmailTemplate";
            if (!_config.TempaltesLocations.TryGetValue(temaplateName, out templatePath))
            {
                throw new EmailTemplatePathException(string.Format(Resources.UnspecifiedPathToTheMessageTemplate, nameof(_config.TempaltesLocations), temaplateName));
            }

            var htmlBody = string.Empty;

            StreamReader sourceReader = null;
            try
            {
                sourceReader = File.OpenText(templatePath);
                htmlBody = sourceReader.ReadToEnd();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(string.Format(Resources.TemplateFileNotFoundExceptionMessage, temaplateName));
            }
            finally
            {
                sourceReader?.Close();
                sourceReader?.Dispose();
            }

            htmlBody = string.Format(htmlBody, _config.WebApiUri.Uri, HttpUtility.UrlEncode(model.Email), model.Token);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Media shop", ((NetworkCredential)_config.Credentials).UserName));
            message.To.Add(new MailboxAddress(model.Email));
            message.Subject = "Password restore";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;

            message.Body = builder.ToMessageBody();

            SendEmail(message);
        }

        protected virtual void Dispose(bool flag)
        {
            if (_disposed)
            {
                return;
            }

            _smtpClient?.Disconnect(true);
            _smtpClient?.Dispose();
            _disposed = true;

            if (flag)
            {
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Method trying sand email
        /// </summary>
        /// <param name="message">MimeMessage message</param>
        /// <param name="tryCount">Count of attempts </param>
        private void SendEmail(MimeMessage message, int tryCount = 0)
        {
            if (tryCount > SendEmailTryCount)
            {
                throw new CountOfTryToEmailSendException(string.Format(Resources.CountOfTryToEmailSendExceptionMessage, SendEmailTryCount));
            }

            try
            {
                _smtpClient.Send(message);
            }
            catch (ObjectDisposedException)
            {
                InitClient();
                SendEmail(message, tryCount++);
            }
            catch (ServiceNotConnectedException)
            {
                ConnectClient();
                SendEmail(message, tryCount++);
            }
            catch (ServiceNotAuthenticatedException)
            {
                AutenticateClient();
                SendEmail(message, tryCount++);
            }
        }

        private void InitClient()
        {
            _smtpClient = new SmtpClient();
        }

        private void ConnectClient(byte tryCount = 0)
        {
            if (tryCount > SendEmailTryCount)
            {
                throw new CountOfTryToEmailSendException(string.Format(Resources.CountOfTryToEmailSendExceptionMessage, SendEmailTryCount));
            }

            try
            {
                _smtpClient.Connect(_config.SmtpHost, _config.SmtpPort);
            }
            catch (OperationCanceledException)
            {
                ConnectClient(tryCount);
            }
        }

        private void AutenticateClient(byte tryCount = 0)
        {
            if (tryCount > SendEmailTryCount)
            {
                throw new CountOfTryToEmailSendException(string.Format(Resources.CountOfTryToEmailSendExceptionMessage, SendEmailTryCount));
            }

            try
            {
                _smtpClient.Authenticate(_config.Credentials);
            }
            catch (AuthenticationException)
            {
                AutenticateClient(tryCount);
            }
        }
    }
}