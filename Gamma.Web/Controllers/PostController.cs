using Gamma.BusinessLogic.DBModel;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Post;
using Gamma.Web.Extensions;
using Gamma.Web.Filters;
using Gamma.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Gamma.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly ISession _session;
        private readonly IPost _post;
        public PostController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
        }
        [AuthorizedMod]
        public ActionResult AddPost()
        {
            return View();
        }
        [HttpPost]
        [AuthorizedMod]
        public ActionResult AddPost(PostData postData)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var author = _session.GetUserById(user.Id);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(postData.Image.FileName);
                    string extension = Path.GetExtension(postData.Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    postData.ImagePath = "~/Content/PostsImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/PostsImages/"), fileName);
                    postData.Image.SaveAs(fileName);
                    PDbModel newPost = new PDbModel
                    {
                        Name = postData.Name,
                        Ingredients = postData.Ingredients,
                        Calories = postData.Calories,
                        ChefSpeciality = postData.ChefSpeciality,
                        Rating = postData.Rating,
                        SpicinessLevel = postData.SpicinessLevel,
                        DietaryRestrictions = postData.DietaryRestrictions,
                        Price = postData.Price,
                        Cuisine = postData.Cuisine,
                        Comment = postData.Comment,
                        ImagePath = postData.ImagePath,
                        DateAdded = DateTime.Now,
                        Author = author.UserName
                    };
                    var response = _post.AddPostAction(newPost);
                    if (response.Status)
                    {
                        return RedirectToAction("Detail", "Post", new { PostID = newPost.Id });
                    }
                    else
                    {
                        ModelState.AddModelError("", response.StatusMessage);
                        return View(postData);
                    }
                }
            }
            return View();
        }
        [AuthorizedMod]
        public ActionResult EditPost(int? postId)
        {
            var postToEdit = _post.GetById((int)postId);
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user != null && user.Username == postToEdit.Author)
            {
                if (ModelState.IsValid)
                {
                    PostData postName = new PostData
                    {
                        Id = postToEdit.Id,
                        Name = postToEdit.Name,
                        Ingredients = postToEdit.Ingredients,
                        Calories = postToEdit.Calories,
                        ChefSpeciality = postToEdit.ChefSpeciality,
                        Rating = postToEdit.Rating,
                        SpicinessLevel = postToEdit.SpicinessLevel,
                        DietaryRestrictions = postToEdit.DietaryRestrictions,
                        Price = postToEdit.Price,
                        Cuisine = postToEdit.Cuisine,
                        Comment = postToEdit.Comment,
                        ImagePath = postToEdit.ImagePath,
                        DateAdded = DateTime.Now,
                        Author = user.Username
                    };
                    return View(postName);
                }
            }
            return View();
        }
        [AuthorizedMod]
        [HttpPost]
        public ActionResult EditPost(PostData postToEdit)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user == null && user.Username != postToEdit.Author) return HttpNotFound();
            if (ModelState.IsValid)
            {
                using (var db = new PostContext())
                {
                    var post = db.Posts.Find(postToEdit.Id);
                    if (post != null)
                    {
                        post.Name = postToEdit.Name;
                        post.Ingredients = postToEdit.Ingredients;
                        post.Calories = postToEdit.Calories;
                        post.ChefSpeciality = postToEdit.ChefSpeciality;
                        post.Rating = postToEdit.Rating;
                        post.SpicinessLevel = postToEdit.SpicinessLevel;
                        post.DietaryRestrictions = postToEdit.DietaryRestrictions;
                        post.Price = postToEdit.Price;
                        post.Cuisine = postToEdit.Cuisine;
                        post.Comment = postToEdit.Comment;
                    }
                    if (postToEdit.Image != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(postToEdit.Image.FileName);
                        string extension = Path.GetExtension(postToEdit.Image.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        postToEdit.ImagePath = "~/Content/PostsImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/PostsImages/"), fileName);
                        postToEdit.Image.SaveAs(fileName);
                        post.ImagePath = postToEdit.ImagePath;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Detail", new { postId = post.Id });
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Detail(int postID)
        {
            var data = _post.GetById(postID);
            using (var db = new UserContext())
            {
                var author = db.Users.Where(a => a.UserName == data.Author).FirstOrDefault();
                var Name = new PostData
                {
                    Id = data.Id,
                    Name = data.Name,
                    Ingredients = data.Ingredients,
                    Calories = data.Calories,
                    ChefSpeciality = data.ChefSpeciality,
                    Rating = data.Rating,
                    SpicinessLevel = data.SpicinessLevel,
                    DietaryRestrictions = data.DietaryRestrictions,
                    Price = data.Price,
                    Cuisine = data.Cuisine,
                    Comment = data.Comment,
                    ImagePath = data.ImagePath,
                    DateAdded = data.DateAdded,
                    Author = author.UserName,
                    AuthorPhoneNumber = author.PhoneNumber
                };
                var relatedPosts = _post.GetPostsByIngredientsOrCuisine(Name.Ingredients)
                    .Concat(_post.GetPostsByIngredientsOrCuisine(Name.Cuisine))
                    .Distinct()
                    .ToList();
                var posts = relatedPosts.GroupBy(a => a.Id)
                    .Select(a => a.First())
                    .Where(a => a.Id != Name.Id)
                    .ToList();
                ViewBag.RelatedPosts = posts;
                return View(Name);
            }
        }
        [AuthorizedMod]
        public ActionResult Delete(int? postId)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var postToDelete = _post.GetById((int)postId);
            if (postToDelete == null || user.Username != postToDelete.Author)
            {
                return HttpNotFound();
            }
            else
            {
                _post.Delete((int)postId);
                _post.Save();
                return RedirectToAction("ActivePosts", "Dashboard");
            }
        }
    }
}