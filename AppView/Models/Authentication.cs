using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppView.Models
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        // Hàm kiển tra tình trạng login, nếu chưa login sẽ ép phải login
        {
            if(context.HttpContext.Session.GetString("Email") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","Access" },
                        {"Action", "Login" }
                    });
            }
        }
    }
}
