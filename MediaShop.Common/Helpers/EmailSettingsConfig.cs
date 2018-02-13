using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces;

namespace MediaShop.Common.Helpers
{
    public class EmailSettingsConfig : IEmailSettingsConfig
    {
        public EmailSettingsConfig(string smtpHost, int smtpPort, UriBuilder webApiUri, ICredentials credintails, IDictionary<string, string> tempaltesLocations)
        {
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            Credentials = credintails;
            TempaltesLocations = tempaltesLocations;
            WebApiUri = webApiUri;
        }

        public EmailSettingsConfig(string smtpHost, int smtpPort, UriBuilder webApiUri, string login, string pwd, IDictionary<string, string> tempaltesLocations) : this(smtpHost, smtpPort, webApiUri, new NetworkCredential(login, pwd), tempaltesLocations)
        {
        }

        private EmailSettingsConfig()
        {
        }

        public string SmtpHost { get; private set; }

        public int SmtpPort { get; private set; }

        public IDictionary<string, string> TempaltesLocations { get; private set; }

        public UriBuilder WebApiUri { get; private set; }

        public ICredentials Credentials { get; private set; }
    }
}