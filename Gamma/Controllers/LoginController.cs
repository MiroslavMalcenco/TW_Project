using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Gamma.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessionIDManager _session;
        public LoginController()
        {
            var bl = new BussinesLogic();
            _session = new ISessionIDManager();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}