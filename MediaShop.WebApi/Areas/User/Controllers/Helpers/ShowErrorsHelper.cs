using System.Text;
using System.Web.Http.ModelBinding;

namespace MediaShop.WebApi.Areas.User.Controllers.Helpers
{
    public static class ShowErrorsHelper
    {
        public static string ShowErrors(ModelStateDictionary modelState)
        {
            var sb = new StringBuilder();
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    sb.AppendFormat($"{error.ErrorMessage} ! ");
                }
            }

            return sb.ToString();
        }
    }
}