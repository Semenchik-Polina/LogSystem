using LogSystem.PL.Filters;
using LogSystem.PL.Utils;
using LogSystem.PL.Models;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.DTO.UserActionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LogSystem.PL.Controllers
{
    [UserAuthorize(Roles = "Admin")]
    public class UserActionController : Controller
    {
        private IUserActionService UserActionService { get; set; }
        private AMapper AMapper { get; set; }

        public UserActionController(IUserActionService userActionService)
        {
            UserActionService = userActionService;
            AMapper = new AMapper();
        }

        public async Task<ActionResult> GetAll()
        {
            var userActionDTOs = await UserActionService.GetAllUserActions();
            var userActionVMs = AMapper.Mapper.Map<IEnumerable<UserActionGetDetailDTO>, IEnumerable<UserActionViewModel>>(userActionDTOs);
            return View("UserActionList", userActionVMs);
        }

        // GET: UserAction
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserAction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAction/Create
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

        // GET: UserAction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserAction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAction/Delete/5
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
