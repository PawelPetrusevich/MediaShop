using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Helpers
{
    public static class NotificationHelper
    {
        public static string FormatAddProductToCartMessage(string productName)
        {
            return string.Format(Resources.AddContentToCartNotificationTemplate, productName);
        }
    }
}
