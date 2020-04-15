using LogSystem.PL.Utils;
using LogSystem.PL.Models;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.DTO.UserActionDTO;
using LogSystem.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;

namespace LogSystem.PL.Controllers.api
{
    [RoutePrefix("api/UserActions")]
    [Authorize]
    public class UserActionController : ApiController
    {
        private IUserActionService UserActionService { get; set; }
        private AMapper AMapper { get; set; }

        public UserActionController(IUserActionService userActionService)
        {
            UserActionService = userActionService;
            AMapper = new AMapper();
        }

        // GET, HEAD: api/UserActions
        [Route]
        [AcceptVerbs("GET", "HEAD")]
        public async Task<IHttpActionResult> GetAsync()
        {
            // check user type by UserType field of UserCookie
            // if the user is not an admin return 403 Forbidden
            CookieHeaderValue cookie = Request.Headers.GetCookies(BLL.Utils.UserCookieHelper.userCookieName).FirstOrDefault();
            if (cookie != null && cookie[BLL.Utils.UserCookieHelper.userCookieName].Values["UserType"] != UserType.Admin.ToString())
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            // return user actions
            var userActionDTOs = await UserActionService.GetAllUserActions();
            var userActionVMs = AMapper.Mapper.Map<IEnumerable<UserActionGetDetailDTO>, IEnumerable<UserActionViewModel>>(userActionDTOs);
            if (userActionVMs.Count() == 0)
            {
                return NotFound();
            }
            return  Ok(userActionVMs);
        }

    }
}
