using System.Web.Mvc;
using Gamma.Web.Models;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.User;

namespace Gamma.Web.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly ISession _session;
        public RegisterController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(RegisterData data)
        {
            if (ModelState.IsValid)
            {
                URegisterData newUser = new URegisterData
                {
                    Email = data.Email,
                    Password = data.Password,
                    UserName = data.UserName,
                    FullName = data.FullName,
                    Terms = data.Terms,
                    PhoneNumber = data.PhoneNumber,
                    IP = Request.UserHostAddress
                };
                var response = _session.ValidateUserRegister(newUser);
                if (response.Status)
                {
                    return RedirectToAction("Index", "Login");
                }
                else 
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }
            return View();
        }
    }
}