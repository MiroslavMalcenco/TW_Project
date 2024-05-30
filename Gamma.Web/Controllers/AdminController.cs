using Gamma.BusinessLogic.DBModel;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Post;
using Gamma.Domain.Entities.User;
using Gamma.Web.Extensions;
using Gamma.Web.Filters;
using Gamma.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Gamma.Web.Controllers
{
    [AdminMod]
    public class AdminController : BaseController
    {
        public readonly ISession _session;
        public readonly IPost _post;
        private readonly UserMinimal adminAuthenticated;
        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            adminAuthenticated = System.Web.HttpContext.Current.GetMySessionObject();
        }
        public ActionResult Dashboard()
        {
            var userData = _session.GetUserById(adminAuthenticated.Id);
            if (adminAuthenticated != null)
            {
                var userName = new UserData()
                {
                    Id = userData.Id,
                    PhoneNumber = userData.PhoneNumber,
                    Username = userData.UserName,
                    FullName = userData.FullName,
                    Email = adminAuthenticated.Email
                };
                return View(userName);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult Users()
        {
            var users = _session.GetAll();
            List<UserData> allUsers = new List<UserData>();
            foreach(var user in users)
            {
                var userData = new UserData();
                userData.Id = user.Id;
                userData.Username = user.UserName;
                userData.AccessLevel = user.AccessLevel;
                userData.Email = user.Email;
                userData.FullName = user.FullName;
                allUsers.Add(userData);
            }
            return View(allUsers);
        }
        public ActionResult EditUser(int? userId)
        {
            if (userId == null) return View();
            var userData = _session.GetUserById((int)userId);
            if (userData != null)
            {
                var userName = new UserData()
                {
                    Id = userData.Id,
                    PhoneNumber = userData.PhoneNumber,
                    Username = userData.UserName,
                    FullName = userData.FullName,
                    Email = userData.Email
                };
                return View(userName);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult EditUser(UserData data)
        {   
            if (ModelState.IsValid)
            {
                UEditProfileData existingUser = new UEditProfileData
                {
                    Id = data.Id,
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    FullName = data.FullName
                };
                var response = _session.EditProfileAction(existingUser);
                if (response.Status)
                {
                    return RedirectToAction("Users");
                }
                else
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }
            return View();
        }
        public ActionResult DeleteUser(int? userId)
        {
            using (var db = new UserContext())
            {
                var userToDelete = db.Users.Find((int)userId);
                if (userToDelete == null)
                {
                    return HttpNotFound();
                }
                db.Users.Remove(userToDelete);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
        }
        public ActionResult Posts()
        {
            var data = _post.GetAll();
            List<PostMinimal> allPosts = new List<PostMinimal>();
            foreach (var post in data)
            {
                var postMinimal = new PostMinimal();
                postMinimal.Id = post.Id;
                postMinimal.DietaryRestrictions = post.DietaryRestrictions;
                postMinimal.Calories = post.Calories;
                postMinimal.ChefSpeciality = post.ChefSpeciality;
                postMinimal.Price = post.Price;
                postMinimal.DateAdded = post.DateAdded;
                postMinimal.SpicinessLevel = post.SpicinessLevel;
                postMinimal.Name = post.Name;
                postMinimal.Ingredients = post.Ingredients;
                postMinimal.Cuisine = post.Cuisine;
                postMinimal.ImagePath = post.ImagePath;
                postMinimal.Rating = post.Rating;
                allPosts.Add(postMinimal);
            }
            return View(allPosts);
        }
        public ActionResult DeletePost(int? postId)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var postToDelete = _post.GetById((int)postId);
            if (postToDelete == null && user.Username == postToDelete.Author)
            {
                return HttpNotFound();
            }
            else
            {
                _post.Delete((int)postId);
                _post.Save();
                return RedirectToAction("Posts", "Admin");
            }
        }
    }
}