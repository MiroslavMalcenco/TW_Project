using System;
using System.Web.Mvc;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Response;
using Gamma.Domain.Entities.User;
using Gamma.Web.Models;

namespace Gamma.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        public ActionResult Index()
        {
            var user = new ULoginData { Password = "12345654", UserName = "user2024", LastLoginTime = DateTime.Now };
            ServiceResponse UValidate = _session.ValidateUserCredential(user);
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginData data)
        {
            if (ModelState.IsValid)
            {
                ULoginData uLogin = new ULoginData
                {
                    UserName = data.Username,
                    Password = data.Password,
                    LastLoginTime = DateTime.Now,
                    IP = Request.UserHostAddress
                };
                var response = _session.ValidateUserCredential(uLogin);
                if (response.Status)
                {
                    var cookieResponse = _session.GenCookie(data.Username);
                    if (cookieResponse != null)
                    {
                        ControllerContext.HttpContext.Response.Cookies.Add(cookieResponse.Cookie);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    ViewBag.Error = "Неправильное имя или пароль";
                    ModelState.AddModelError("Неправильное имя или пароль", response.StatusMessage);
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginData());
        }
    }
}