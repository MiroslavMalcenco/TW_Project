using System.Linq;
using System.Web.Mvc;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Post;
using Gamma.Web.Filters;
using Gamma.Web.Models;

namespace Gamma.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPost _post;
        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _post = bl.GetPostBL();
        }
        [LatestPosts]
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
        [LatestPosts] 
        [HttpGet]
        public ActionResult Index(SearchWrapData searchWrapData)
        {
            if (searchWrapData.IngredientsOrName != null)
            {
                PSearchWrapData data = new PSearchWrapData
                {
                    IngredientsOrName = searchWrapData.IngredientsOrName,
                    PriceRange = searchWrapData.PriceRange,
                    Cuisine = searchWrapData.Cuisine
                };
                var results = _post.GetBySearchWrapData(data);
                if (results.Count() > 0)
                {
                    TempData["foundPosts"] = results;
                    return RedirectToAction("ListingParameters", "Listing");
                }
                else
                {
                    return RedirectToAction("ListingParameters", "Listing");
                }
            }
            else
            {
                return View();
            }
        }
    }
}