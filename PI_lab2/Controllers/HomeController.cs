using System.Web.Mvc;

namespace PI_lab2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }
    }
}