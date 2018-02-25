using System;
using System.Collections.Generic;
using System.Net;

namespace MediaShop.Common.Interfaces
{
    public interface IEmailSettingsConfig
    {
        ICredentials Credentials { get; }

        string SmtpHost { get; }

        int SmtpPort { get; }

        IDictionary<string, string> TempaltesLocations { get; }
    }
}