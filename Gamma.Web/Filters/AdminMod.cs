using Gamma.BusinessLogic.Interfaces;
using Gamma.Web.Extensions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamma.Web.Filters
{
    public class AdminMod : ActionFilterAttribute
    {
        private readonly ISession _session;
        public AdminMod()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var user = _session.GetUserByCookie(apiCookie.Value);
                if (user != null && user.Level == Domain.Enum.URole.ADMINISTRATOR)
                {
                    HttpContext.Current.SetMySessionObject(user);
                    filterContext.Controller.ViewBag.AuthorizedUser = user;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
        }
    }
}