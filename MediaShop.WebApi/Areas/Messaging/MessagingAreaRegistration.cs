﻿using System.Web.Mvc;

namespace MediaShop.WebApi.Areas.Messaging
{
    public class MessagingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Messaging";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Messaging_default",
                "Messaging/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}