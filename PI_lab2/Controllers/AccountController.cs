using System.Web.Mvc;
using System.Web.Security;
using PI_lab2.Data;
using PI_lab2.Models.AccountModels;
using PI_lab2.PIMembership;

namespace PI_lab2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
            var membershipUser = ((PiMembershipProvider)Membership.Provider).CreateUser(model.Email, model.Password, model.Login);
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(model.Login, false);
            return RedirectToAction("Index", "Account");
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
            if (Membership.ValidateUser(model.Email, model.Password))
            {
                var login = DataManager.GetLogin(model.Email);
                FormsAuthentication.SetAuthCookie(login, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Wrong email or password.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public ActionResult Manage()
        //{
        //    var client = new UserManagerServiceClient();
        //    try
        //    {
        //        var user = client.GetUser(User.Identity.Name);
        //        var userManageView = ViewModelConverter.ConvertToUserManageViewModel(user);
        //        return System.Web.UI.WebControls.View(userManageView);
        //    }
        //    catch (TimeoutException)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (FaultException)
        //    {
        //        FormsAuthentication.SignOut();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (CommunicationException)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //[HttpPost]
        //public ActionResult Manage(UserManageViewModel model)
        //{
        //    if (!ModelState.IsValid) return System.Web.UI.WebControls.View(model);
        //    byte[] photoBytes;
        //    if (model.Photo != null)
        //    {
        //        if (model.Photo.ContentLength > (4 * 1024 * 1024))
        //        {
        //            ModelState.AddModelError("PhotoError", "Image can not be lager than 4MB.");
        //            return View();
        //        }
        //        if (model.Photo.ContentType != "image/jpeg")
        //        {
        //            ModelState.AddModelError("PhotoError", "Image must be in jpeg format.");
        //            return View();
        //        }
        //        photoBytes = new byte[model.Photo.ContentLength];
        //        model.Photo.InputStream.Read(photoBytes, 0, model.Photo.ContentLength);
        //    }
        //    else
        //    {
        //        photoBytes = null;
        //    }

        //    byte[] backgroundBytes;
        //    if (model.Background != null)
        //    {
        //        if (model.Background.ContentLength > (20 * 1024 * 1024))
        //        {
        //            ModelState.AddModelError("BackgroundError", "Image can not be lager than 20MB.");
        //            return View();
        //        }
        //        if (model.Background.ContentType != "image/jpeg")
        //        {
        //            ModelState.AddModelError("BackgroundError", "Image must be in jpeg format.");
        //            return View();
        //        }
        //        backgroundBytes = new byte[model.Background.ContentLength];
        //        model.Background.InputStream.Read(backgroundBytes, 0, model.Background.ContentLength);
        //    }
        //    else
        //    {
        //        backgroundBytes = null;
        //    }

        //    var client = new UserManagerServiceClient();
        //    try
        //    {
        //        client.UpdateUser(model.Email, model.Nickname, photoBytes, backgroundBytes, model.Name, model.AboutMe, model.Status);
        //        FormsAuthentication.SignOut();
        //        FormsAuthentication.SetAuthCookie(model.Nickname, true);
        //        return RedirectToAction("Index", "Account", new { nickname = model.Nickname });
        //    }
        //    catch (TimeoutException)
        //    {
        //        ModelState.AddModelError("", "The service operation timed out.");
        //        return System.Web.UI.WebControls.View(model);
        //    }
        //    catch (FaultException exception)
        //    {
        //        ModelState.AddModelError("", exception.Message);
        //        return System.Web.UI.WebControls.View(model);
        //    }
        //    catch (CommunicationException)
        //    {
        //        ModelState.AddModelError("", "There was a communication problem.");
        //        return System.Web.UI.WebControls.View(model);
        //    }
        //}
    }
}