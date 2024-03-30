using Gamma.BusinessLogic.Interfaces;
using System.Web.Mvc;

namespace Gamma.Web.Filters
{
    public class LatestPostsAttribute : ActionFilterAttribute
    {
        private IPost _post;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var bl = new BusinessLogic.BusinessLogic();
            _post = bl.GetPostBL();
            var latestRecords = _post.GetLatestPosts();
            filterContext.Controller.ViewBag.LatestRecords = latestRecords;
        }
    }
}