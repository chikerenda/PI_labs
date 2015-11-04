using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PI_lab2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Account", "Login");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UserList()
        {
            
        }
    }
}