using System;
using System.Diagnostics.Eventing.Reader;
using System.Web.Mvc;
using System.Web.Security;
using PI_lab2.MembershipPi;
using PI_lab2.Models;
using PI_lab2.Models.DataModels;

namespace PI_lab2.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View(DataManager.GetAllUsers());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var membershipUser = ((PiMembershipProvider)Membership.Provider).CreateUser(model.Email, model.Password, model.Login);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(model.Login, false);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            if (Membership.ValidateUser(model.Login, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Wrong login or password.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(User user)
        {
            DataManager.AddUser(user);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Edit(string login)
        {
            if (User.IsInRole("Admin") || User.Identity.Name == login)
            {
                return View(DataManager.GetUser(login));
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            DataManager.EditUser(user);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string login)
        {
            return View(new DeleteModel {Login = login});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(DeleteModel model)
        {
            DataManager.DeleteUser(model.Login);
            if (model.Login == User.Identity.Name)
            {
                return RedirectToAction("LogOff", "User");
            }
            return RedirectToAction("Index", "User");
        }


    }
}