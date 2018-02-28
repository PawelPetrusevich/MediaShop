using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MediaShop.Common.Helpers
{
    public static class EmailSettingsConfigHelper
    {
        public static EmailSettingsConfig InitWithAppConf()
        {
            var email = ConfigurationManager.AppSettings["AppEmail"];
            var emailPvd = ConfigurationManager.AppSettings["AppEmailPassword"];
            var emailSmtpHost = ConfigurationManager.AppSettings["AppEmailSmtpHost"];
            var emailSmtpPort = int.Parse(ConfigurationManager.AppSettings["AppEmailSmtpPort"]);

            var temapltesPath = new Dictionary<string, string>();
            var pathFolders = AppContext.BaseDirectory.Split('\\').ToList();
            pathFolders[0] += '\\';
            if (string.IsNullOrWhiteSpace(pathFolders[pathFolders.Count - 1]))
            {
                pathFolders = pathFolders.Take(pathFolders.Count - 1).ToList();
            }

            pathFolders.Add("Content");
            pathFolders.Add("Templates");
            var templatesFoldePath = Path.Combine(pathFolders.ToArray());
            temapltesPath.Add("AccountConfirmationEmailTemplate", Path.Combine(templatesFoldePath, "AccountConfirmationEmailTemplate.html"));
            temapltesPath.Add("AccountPwdRestoreEmailTemplate", Path.Combine(templatesFoldePath, "AccountPwdRestoreEmailTemplate.html"));
            return new EmailSettingsConfig(emailSmtpHost, emailSmtpPort, email, emailPvd, temapltesPath);
        }
    }
}