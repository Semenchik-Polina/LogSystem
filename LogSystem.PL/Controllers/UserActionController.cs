using LogSystem.PL.Filters;
using LogSystem.PL.Utils;
using LogSystem.PL.Models;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.DTO.UserActionDTO;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Get()
        {
            var userActionDTOs = await UserActionService.GetAllUserActions();
            var userActionVMs = AMapper.Mapper.Map<IEnumerable<UserActionGetDetailDTO>, IEnumerable<UserActionViewModel>>(userActionDTOs);
            return Json(userActionVMs, JsonRequestBehavior.AllowGet);
                //View("UserActionList", userActionVMs);
        }
    }
}
