using System.Web.Mvc;

namespace MediaShop.WebApi.Areas.Payments
{
    public class PaymentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Payments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Payments_default",
                "Payments/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}