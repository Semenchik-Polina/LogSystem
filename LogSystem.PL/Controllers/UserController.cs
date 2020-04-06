using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.PL.Models.UserViewModels;
using LogSystem.PL.Utils;
using LogSystem.PL.Filters;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogSystem.Common.Helpers;

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

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var userGetDetailDTO = await UserService.GetUserByID(id);
            if (userGetDetailDTO == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            var userVM = AMapper.Mapper.Map<UserGetDetailDTO, UserEditorViewModel>(userGetDetailDTO);

            return View(userVM);
        }

        // POST: User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int UserID, UserEditorViewModel user)
        {
            try
            {
                UserEditDTO userUpdateDTO = AMapper.Mapper.Map<UserEditorViewModel, UserEditDTO>(user);
                await UserService.EditUser(userUpdateDTO);

                var updatedUserCookie = BLL.Utils.UserCookieHelper.UpdateUserCookie(Response.Cookies[BLL.Utils.UserCookieHelper.userCookieName], userUpdateDTO);

                Response.Cookies.Add(updatedUserCookie);

                return RedirectToAction("Edit", new { id = UserID });
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
