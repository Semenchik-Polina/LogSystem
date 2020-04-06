using LogSystem.Common.Enums;
using System;
using System.Web.Mvc;
using LogSystem.BLL.Utils;
using System.Web.Security;
using LogSystem.PL.Filters;
using LogSystem.BLL.Interfaces;
using System.Threading.Tasks;

namespace LogSystem.PL.Controllers
{
    [UserAuthorize]
    public class MainController : Controller
    {
        private IUserService UserService { get; set; }

        public MainController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public ActionResult GetUserProfile()
        {
            int id = Int32.Parse(Request.Cookies[UserCookieHelper.userCookieName]["UserID"]);
            var userType = Request.Cookies[UserCookieHelper.userCookieName]["UserType"];
            if (Enum.IsDefined(typeof(UserType), userType))
            {
                return RedirectToAction("Edit", "User", new { id });
            }
            else
            {
                ModelState.AddModelError("Profile", "No profile information found");
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        [HttpGet]
        public async Task<ActionResult> SignOut()
        {
            int id = Int32.Parse(Request.Cookies[UserCookieHelper.userCookieName]["UserID"]);
            await UserService.LogOut(id);

            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);

            UserCookieHelper.DeleteUserCookie(Response.Cookies[UserCookieHelper.userCookieName]);

            return Redirect("../LogIn/LogIn");
        }

    }
}