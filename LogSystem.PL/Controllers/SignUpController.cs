using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.PL.Models.UserViewModels;
using LogSystem.PL.Utils;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            if (!ModelState.IsValid)
            {
                return View();
            }

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

            await UserService.SignUp(userCreateDTO);
  
            return RedirectToAction("LogIn", "LogIn");
        }

        [HttpPost]
        public async Task<JsonResult> IsUserNameAvailable(string UserName)
        {
            bool result = await ValidationService.IsUserNameAvailable(UserName);
            return Json(result);
        }

    }
}