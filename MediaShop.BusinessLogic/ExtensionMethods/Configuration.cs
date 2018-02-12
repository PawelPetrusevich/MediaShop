namespace MediaShop.BusinessLogic.ExtensionMethods
{
    using System.Collections.Generic;
    using PayPal.Api;

    public class Configuration
    {
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        /// <summary>
        /// Initializes static members of the <see cref="Configuration"/> class.
        /// </summary>
        static Configuration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        /// <summary>
        /// Create the configuration map that contains mode and other optional configuration details.
        /// </summary>
        /// <returns>Dictionary contains configuration details</returns>
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        /// <summary>
        /// Returns APIContext object
        /// </summary>
        /// <param name="accessToken">accessToken</param>
        /// <returns>new ApiContext</returns>
        public static APIContext GetAPIContext(string accessToken = "")
        {
            var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ? GetAccessToken() : accessToken);
            apiContext.Config = GetConfig();
            return apiContext;
        }

        /// <summary>
        /// Create accessToken
        /// </summary>
        /// <returns>accessToken</returns>
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
    }
}
