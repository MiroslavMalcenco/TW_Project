using System.Web.Mvc;

namespace Gamma.Web.Controllers
{
    public class AboutUsController : BaseController
    {
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult FAQs()
        {
            return View();
        }
    }
}