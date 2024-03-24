using Gamma.BuisnessLogic;
using Gamma.BuisnessLogic.Interfaces;
using System.Web.Mvc;
using System.Web.SessionState;


namespace Gamma.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}