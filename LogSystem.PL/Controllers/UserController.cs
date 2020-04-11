using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.PL.Models.UserViewModels;
using LogSystem.PL.Utils;
using LogSystem.PL.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;
using LogSystem.Common.Helpers;
using System.Web.Script.Serialization;

namespace LogSystem.PL.Controllers
{
    [UserAuthorize]
    public class UserController : Controller
    {
        private IUserService UserService { get; set; }
        private IValidationService ValidationService { get; set; }
        private AMapper AMapper { get; set; }

        public UserController(IUserService userService, IValidationService valService)
        {
            UserService = userService;
            ValidationService = valService;
            AMapper = new AMapper();
        }

        // GET: User/Get/5
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var userGetDetailDTO = await UserService.GetUserByID(id);
            if (userGetDetailDTO == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            var userVM = AMapper.Mapper.Map<UserGetDetailDTO, UserEditViewModel>(userGetDetailDTO);
            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var userGetDetailDTO = await UserService.GetUserByID(id);
            //if (userGetDetailDTO == null)
            //{
            //    return RedirectToAction("LogIn", "LogIn");
            //}
            //var userVM = AMapper.Mapper.Map<UserGetDetailDTO, UserEditViewModel>(userGetDetailDTO);
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int UserID, UserEditViewModel user)
        {
            try
            {
                UserUpdateDTO userUpdateDTO = AMapper.Mapper.Map<UserEditViewModel, UserUpdateDTO>(user);
                await UserService.UpdateUser(userUpdateDTO);

                var updatedUserCookie = BLL.Utils.UserCookieHelper.UpdateUserCookie(Response.Cookies[BLL.Utils.UserCookieHelper.userCookieName], userUpdateDTO);

                Response.Cookies.Add(updatedUserCookie);

                return Json(UserID, JsonRequestBehavior.AllowGet);

                //return RedirectToAction("Edit", new { id = UserID });
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
