using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
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
            var htmlBody = GetTemplateText("AccountConfirmationEmailTemplate");
            htmlBody = string.Format(htmlBody, model.Origin, HttpUtility.UrlEncode(model.Email), HttpUtility.UrlEncode(model.Token));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Media shop", ((NetworkCredential)_config.Credentials).UserName));
            message.To.Add(new MailboxAddress(model.Email));
            message.Subject = "Email confirmation";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;

            message.Body = builder.ToMessageBody();
            ConnectClient();
            AutenticateClient();
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
            var htmlBody = GetTemplateText("AccountPwdRestoreEmailTemplate");
            htmlBody = string.Format(htmlBody, model.Origin, HttpUtility.UrlEncode(model.Email), HttpUtility.UrlEncode(model.Token));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Media shop", ((NetworkCredential)_config.Credentials).UserName));
            message.To.Add(new MailboxAddress(model.Email));
            message.Subject = "Password restore";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;

            message.Body = builder.ToMessageBody();
            ConnectClient();
            AutenticateClient();
            SendEmail(message);
        }

        public async Task SendConfirmationAsync(AccountConfirmationDto model)
        {
            var htmlBody = await GetTemplateTextAsync("AccountConfirmationEmailTemplate").ConfigureAwait(false);
            htmlBody = string.Format(htmlBody, model.Origin, HttpUtility.UrlEncode(model.Email), HttpUtility.UrlEncode(model.Token));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Media shop", ((NetworkCredential)_config.Credentials).UserName));
            message.To.Add(new MailboxAddress(model.Email));
            message.Subject = "Email confirmation";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;

            message.Body = builder.ToMessageBody();

            await ConnectClientAsync().ContinueWith(t => AutenticateClientAsync().ContinueWith(tt => SendEmailAsync(message))).ConfigureAwait(false);
        }

        public async Task SendRestorePwdLinkAsync(AccountPwdRestoreDto model)
        {
            var htmlBody = await GetTemplateTextAsync("AccountPwdRestoreEmailTemplate").ConfigureAwait(false);
            htmlBody = string.Format(htmlBody, model.Origin, HttpUtility.UrlEncode(model.Email), HttpUtility.UrlEncode(model.Token));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Media shop", ((NetworkCredential)_config.Credentials).UserName));
            message.To.Add(new MailboxAddress(model.Email));
            message.Subject = "Password restore";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;

            message.Body = builder.ToMessageBody();

            await ConnectClientAsync().ContinueWith(t => AutenticateClientAsync().ContinueWith(tt => SendEmailAsync(message))).ConfigureAwait(false);
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

        private string GetTemplateText(string templateName)
        {
            string templatePath;
            string templateText = string.Empty;
            if (!_config.TempaltesLocations.TryGetValue(templateName, out templatePath))
            {
                throw new EmailTemplatePathException(string.Format(Resources.UnspecifiedPathToTheMessageTemplate, nameof(_config.TempaltesLocations), templateName));
            }

            StreamReader sourceReader = null;
            try
            {
                sourceReader = File.OpenText(templatePath);
                templateText = sourceReader.ReadToEnd();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(string.Format(Resources.TemplateFileNotFoundExceptionMessage, templateName));
            }
            finally
            {
                sourceReader?.Close();
                sourceReader?.Dispose();
            }

            return templateText;
        }

        private async Task<string> GetTemplateTextAsync(string templateName)
        {
            string templatePath;
            Task<string> templateText;
            if (!_config.TempaltesLocations.TryGetValue(templateName, out templatePath))
            {
                throw new EmailTemplatePathException(string.Format(Resources.UnspecifiedPathToTheMessageTemplate, nameof(_config.TempaltesLocations), templateName));
            }

            StreamReader sourceReader = null;
            try
            {
                sourceReader = File.OpenText(templatePath);
                return await sourceReader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(string.Format(
                    Resources.TemplateFileNotFoundExceptionMessage,
                    templateName));
            }
            finally
            {
                sourceReader?.Close();
                sourceReader?.Dispose();
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

        private async Task<Task> SendEmailAsync(MimeMessage message, int tryCount = 0)
        {
            if (tryCount > SendEmailTryCount)
            {
                throw new CountOfTryToEmailSendException(string.Format(Resources.CountOfTryToEmailSendExceptionMessage, SendEmailTryCount));
            }

            try
            {
                return _smtpClient.SendAsync(message);
            }
            catch (ObjectDisposedException)
            {
                InitClient();
                return SendEmailAsync(message, tryCount++);
            }
            catch (ServiceNotConnectedException)
            {
                await ConnectClientAsync();
                return SendEmailAsync(message, tryCount++);
            }
            catch (ServiceNotAuthenticatedException)
            {
                await AutenticateClientAsync();
                return SendEmailAsync(message, tryCount++);
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

        private Task ConnectClientAsync(byte tryCount = 0)
        {
            if (tryCount > SendEmailTryCount)
            {
                throw new CountOfTryToEmailSendException(string.Format(Resources.CountOfTryToEmailSendExceptionMessage, SendEmailTryCount));
            }

            try
            {
                return _smtpClient.ConnectAsync(_config.SmtpHost, _config.SmtpPort);
            }
            catch (OperationCanceledException)
            {
                return ConnectClientAsync(tryCount++);
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

        private Task AutenticateClientAsync(byte tryCount = 0)
        {
            if (tryCount > SendEmailTryCount)
            {
                throw new CountOfTryToEmailSendException(string.Format(Resources.CountOfTryToEmailSendExceptionMessage, SendEmailTryCount));
            }

            try
            {
                return _smtpClient.AuthenticateAsync(_config.Credentials);
            }
            catch (AuthenticationException)
            {
                return AutenticateClientAsync(tryCount++);
            }
        }
    }
}