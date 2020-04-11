using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.PL.Models.UserViewModels;
using LogSystem.PL.Utils;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace LogSystem.PL.Controllers
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        private IUserService UserService { get; set; }
        private IValidationService ValidationService { get; set; }
        private AMapper AMapper { get; set; }

        public SignUpController(IUserService userService, IValidationService valService)
        {
            UserService = userService;
            ValidationService = valService;
            AMapper = new AMapper();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(RegistrationViewModel regVM)
        {
            // remove ???
            //if (!ModelState.IsValid)
            //{
            //    return Json(null, JsonRequestBehavior.AllowGet);
            //    //return View();
            //}

            UserCreateDTO userCreateDTO = AMapper.Mapper.Map<RegistrationViewModel, UserCreateDTO>(regVM);
            //var error = await ValidationService.ValidateSignUpUser(userCreateDTO);

            //if (error != null)
            //{
            //    ModelState.AddModelError("Email", error.description);
            //}
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            int newUserId = await UserService.SignUp(userCreateDTO);

            // TODO: return added user
            return Json(newUserId, JsonRequestBehavior.AllowGet);

            //return new HttpStatusCodeResult(HttpStatusCode.Created);

            //return RedirectToAction("LogIn", "LogIn");
        }

        [HttpPost]
        public async Task<ActionResult> IsUserNameTaken(string UserName)
        {
            bool result = await ValidationService.IsUserNameTaken(UserName);
            return Json(result);
        }

    }
}