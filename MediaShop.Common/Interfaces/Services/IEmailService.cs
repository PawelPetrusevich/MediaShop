using System;
using System.IO;
using System.Threading.Tasks;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Exceptions.NotificationExceptions;

namespace MediaShop.Common.Interfaces.Services
{
    /// <summary>
    /// Interface IEmailService
    /// </summary>
    public interface IEmailService : IDisposable
    {
        /// <summary>
        /// Method for send account confirmation
        /// </summary>
        /// <exception cref="EmailTemplatePathException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CountOfTryToEmailSendException"></exception>
        /// <param name="model">Confirmation model</param>
        void SendConfirmation(AccountConfirmationDto model);

        /// <summary>
        /// Method for send restore link
        /// </summary>
        /// <exception cref="EmailTemplatePathException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CountOfTryToEmailSendException"></exception>
        /// <param name="model"></param>
        void SendRestorePwdLink(AccountPwdRestoreDto model);

        /// <summary>
        /// Method for async send account confirmation
        /// </summary>
        /// <exception cref="EmailTemplatePathException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CountOfTryToEmailSendException"></exception>
        /// <param name="model">Confirmation model</param>
        Task SendConfirmationAsync(AccountConfirmationDto model);

        /// <summary>
        /// Method for async send restore link
        /// </summary>
        /// <exception cref="EmailTemplatePathException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="CountOfTryToEmailSendException"></exception>
        /// <param name="model"></param>
        Task SendRestorePwdLinkAsync(AccountPwdRestoreDto model);
    }
}