using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.Utils;
using LogSystem.Common.Helpers;
using LogSystem.PL.Models.UserViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace LogSystem.PL.Controllers
{
    [AllowAnonymous]
    public class LogInController : Controller
    {
        private IUserService UserService { get; set; }
        private IValidationService ValidationService { get; set; }
        private Utils.AMapper AMapper { get; set; }
        private ErrorHelper ErrorHelper { get; set; }

        public LogInController(IUserService userService, IValidationService validationService)
        {
            UserService = userService;
            ValidationService = validationService;
            AMapper = new Utils.AMapper();
            ErrorHelper = new ErrorHelper();
        }

        // GET: Default     
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(AuthorizationViewModel authVM)
        {
            var error = await ValidationService.ValidateLogInUser(authVM.UserName, authVM.Password);

            if (error != null)
            {
                ModelState.AddModelError("Username", error.description);
            }

            if (!ModelState.IsValid)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                //return View("Get");
            }

            UserLogInDTO userLogInDTO = AMapper.Mapper.Map<AuthorizationViewModel, UserLogInDTO>(authVM);
            UserGetDetailDTO userGetDTO = await UserService.LogIn(userLogInDTO);

            FormsAuthentication.SetAuthCookie(userGetDTO.UserName, true);
            Response.Cookies.Add(UserCookieHelper.CreateUserCookie(userGetDTO));

            return Json(userGetDTO, JsonRequestBehavior.AllowGet);

            //return RedirectToAction("Edit", "User", new { id = userGetDTO.UserID});
        }

    }
}