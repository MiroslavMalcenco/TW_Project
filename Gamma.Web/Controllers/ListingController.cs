using Gamma.BusinessLogic.DBModel;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Post;
using Gamma.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Gamma.Web.Controllers
{
    public class ListingController : BaseController
    {
        private readonly IPost _post;
        public ListingController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _post = bl.GetPostBL();
        }
        public ActionResult ListingSearchWrap()
        {
            var posts = (List<PostMinimal>)TempData["NameList"];
            if (posts.Any())
            {
                return RedirectToAction("ListingParameters");
            }
            return RedirectToAction("ListingParameters");
        }
        [HttpGet]
        public ActionResult ListingSearch(string Ingredients, string Cuisine)
        {
            if (Ingredients != null)
            {
                var data = _post.GetPostsByIngredientsOrCuisine(Ingredients);
                if (data.Count() > 0)
                {
                    TempData["postsByIngredients"] = data;
                    return RedirectToAction("ListingParameters");
                }
            }
            if (Cuisine != null)
            {
                var data = _post.GetPostsByIngredientsOrCuisine(Cuisine);
                if (data.Any())
                {
                    TempData["postsByCuisine"] = data;
                    return RedirectToAction("ListingParameters");
                }
            }
            return RedirectToAction("ListingParameters");
        }
        public ActionResult ListingParameters()
        {
            var Name = new ListingPageData();
            var sideBar = new SideBarData();
            using (var db = new PostContext())
            {
                var Caloriess = db.Posts.Select(a => a.Calories).Distinct().ToList();
                var Ingredientss = db.Posts.Select(a => a.Ingredients).Distinct().ToList();
                var prices = db.Posts.Select(a => a.Price).Distinct().ToList();
                var Cuisines = db.Posts.Select(a => a.Cuisine).Distinct().ToList();
                sideBar.CuisineList = Cuisines;
                sideBar.IngredientsList = Ingredientss;
                sideBar.PriceRange = prices;
                sideBar.CaloriesRange = Caloriess;
            }
            Name.SideBar = sideBar;
            if (TempData["foundPosts"] is List<PostMinimal> postsBySearchWrap && postsBySearchWrap.Any())
            {
                Name.ListingItems = postsBySearchWrap;
                return View(Name);
            }
            else if (TempData["postsListByType"] is List<PostMinimal> postsByType && postsByType.Any())
            {
                Name.ListingItems = postsByType;
                return View(Name);
            }
            else if (TempData["postsByIngredients"] is List<PostMinimal> postsByIngredients && postsByIngredients.Any())
            {
                Name.ListingItems = postsByIngredients;
                return View(Name);
            }
            else if (TempData["postsByCuisine"] is List<PostMinimal> postsByCuisine && postsByCuisine.Any())
            {
                Name.ListingItems = postsByCuisine;
                return View(Name);
            }
            else if (TempData["postsByFilter"] is List<PostMinimal> postsByFilter && postsByFilter.Any())
            {
                Name.ListingItems = postsByFilter;
                return View(Name);
            }
            if (Name.ListingItems == null) return View(Name);
            return View(Name);
        }
        public ActionResult Listing()
        {
            var Name = new ListingPageData();
            var sideBar = new SideBarData();
            using (var db = new PostContext())
            {
                var Caloriess = db.Posts.Select(a => a.Calories).Distinct().ToList();
                var Ingredientss = db.Posts.Select(a => a.Ingredients).Distinct().ToList();
                var prices = db.Posts.Select(a => a.Price).Distinct().ToList();
                var Cuisines = db.Posts.Select(a => a.Cuisine).Distinct().ToList();
                sideBar.CuisineList = Cuisines;
                sideBar.IngredientsList = Ingredientss;
                sideBar.PriceRange = prices;
                sideBar.CaloriesRange = Caloriess;
            }
            Name.SideBar = sideBar;
            var data = _post.GetAll();
            Name.ListingItems = data.Select(post => new PostMinimal
            {
                Id = post.Id,
                DietaryRestrictions = post.DietaryRestrictions,
                Calories = post.Calories,
                ChefSpeciality = post.ChefSpeciality,
                Price = post.Price,
                DateAdded = post.DateAdded,
                SpicinessLevel = post.SpicinessLevel,
                Name = post.Name,
                Ingredients = post.Ingredients,
                Cuisine = post.Cuisine,
                ImagePath = post.ImagePath,
                Rating = post.Rating
            }).ToList();
            return View(Name);
        }
        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Sidebar(ListingPageData data)
        {
            var filter = new PListingFilterData()
            {
                KeyWord = data.SideBar.KeyWord,
                MinPrice = data.SideBar.MinPrice,
                MaxPrice = data.SideBar.MaxPrice,
                Cuisine = data.SideBar.Cuisine,
                Ingredients = data.SideBar.Ingredients,
                MaxCalories = data.SideBar.MaxCalories,
                MinCalories = data.SideBar.MinCalories,
                ChefSpeciality = data.SideBar.ChefSpeciality,
                DietaryRestrictions = data.SideBar.DietaryRestrictions
            };
            TempData["postsByFilter"] = _post.GetPostsByListingFilter(filter);
            return RedirectToAction("ListingParameters");
        }
    }
}