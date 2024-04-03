using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.User;
using Gamma.Web.Extensions;
using Gamma.Web.Filters;
using Gamma.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Gamma.Web.Controllers
{
    public class DashboardController : BaseController
    {
        public readonly ISession _session;
        public readonly IPost _post;
        private readonly UserMinimal userAuthenticated;
        public DashboardController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            userAuthenticated = System.Web.HttpContext.Current.GetMySessionObject();
        }
        [AuthorizedMod]
        public ActionResult Dashboard()
        {
            var userData = _session.GetUserById(userAuthenticated.Id);
            if (userAuthenticated != null)
            {
                var userName = new UserData()
                {
                    Id = userData.Id,
                    PhoneNumber = userData.PhoneNumber,
                    Username = userData.UserName,
                    FullName = userData.FullName,
                    Email = userAuthenticated.Email
                };
                return View(userName);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [AuthorizedMod]
        public ActionResult EditProfile()
        {
            var userName = new UserData();
            return View(userName);
        }
        [AuthorizedMod]
        [HttpGet]
        public ActionResult EditProfile(int? userId)
        {
            if (userId == null)
            {
                userId = userAuthenticated.Id;
            }
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
        [AuthorizedMod]
        [HttpPost]
        public ActionResult EditProfile(UserData data)
        {
            if (userAuthenticated != null && data.Id == userAuthenticated.Id)
            {
                if (ModelState.IsValid)
                {
                    UEditProfileData existingUser = new UEditProfileData
                    {
                        Id = userAuthenticated.Id,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        FullName = data.FullName
                    };
                    var response = _session.EditProfileAction(existingUser);
                    if (response.Status)
                    {
                        return RedirectToAction("Dashboard", "Dashboard", existingUser);
                    }
                    else
                    {
                        ModelState.AddModelError("", response.StatusMessage);
                        return View(data);
                    }
                }
            }
            return View();
        }
        public ActionResult ActivePosts()
        {
            var userName = new UserData();
            return View(userName);
        }
        [AuthorizedMod]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ActivePosts(int? userId)
        {
            if (userId == null)
            {
                userId = userAuthenticated.Id;
            }
            var posts = _post.GetPostsByAuthor(_session.GetUserById((int)userId).UserName);
            if (posts.Count() > 0)
            {
                return View(posts);
            }
            return View();
        }
        [AuthorizedMod]
        public ActionResult ChangePassword()
        {
            var Name = new UChangePasswordData()
            {
                Id = userAuthenticated.Id
            };
            return View(Name);
        }
        [AuthorizedMod]
        [HttpGet]
        public ActionResult ChangePassword(int? userId)
        {
            if (userId == null)
            {
                userId = userAuthenticated.Id;
            }
            if (userId != null)
            {
                var Name = new UChangePasswordData()
                {
                    Id = (int)userId
                };
                return View(Name);
            }
            return View(new UChangePasswordData());
        }
        [AuthorizedMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UChangePasswordData password)
        {
            var Name = new UChangePasswordData();
            if (ModelState.IsValid)
            {
                if (password.NewPassword == password.ConfirmedPassword && userAuthenticated.Id == password.Id)
                {
                    var response = _session.ChangePassword(password);
                    if (response.Status)
                    {
                        ViewBag.ConfirmationMessage = response.StatusMessage;
                        return View(Name);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Подтверждение провалено! Попробуйте еще раз";
                    return View(Name);
                }
            }
            return View();
        }
        [AuthorizedMod]
        public ActionResult UserLogout()
        {
            Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}