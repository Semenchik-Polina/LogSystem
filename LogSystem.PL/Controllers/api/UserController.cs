using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.PL.Models.UserViewModels;
using LogSystem.PL.Utils;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;

namespace LogSystem.PL.Controllers.api
{
    [Authorize]
    [RoutePrefix("api/Users")]
    public class UserController : ApiController
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

        // GET: api/Users/5
        [Route("{id:int}")]
        [AcceptVerbs("GET", "HEAD")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            // check if authenticated user try to get his profile (get by UserCookie)
            // if not, return 403 Forbidden
            CookieHeaderValue cookie = Request.Headers.GetCookies(BLL.Utils.UserCookieHelper.userCookieName).FirstOrDefault();
            if ( cookie != null && Int32.Parse(cookie[BLL.Utils.UserCookieHelper.userCookieName].Values["UserID"]) != id)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            // get the user's profile by id
            var userGetDetailDTO = await UserService.GetUserByID(id);
            if (userGetDetailDTO == null)
            {
                return NotFound();
            }
            var userVM = AMapper.Mapper.Map<UserGetDetailDTO, UserEditViewModel>(userGetDetailDTO);

            return Ok(userVM);
        }

        // GET: api/Users/Cleo
        // get user by UserName
        [AllowAnonymous]
        [Route("{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsyncByUserName(string userName)
        {
            var userGetDetailDTO = await UserService.GetUserByUserName(userName);
            if (userGetDetailDTO == null)
            {
                return Ok();
            }
            var userVM = AMapper.Mapper.Map<UserGetDetailDTO, UserEditViewModel>(userGetDetailDTO);

            return Ok(userVM);
        }
        
        [Route("{id:int}")]
        [HttpPut]
        // PUT: api/Users/5
        public async Task<HttpResponseMessage> PutAsync(int id, [FromBody] UserEditViewModel user)
        {
            // update user's profile
            UserUpdateDTO userUpdateDTO = AMapper.Mapper.Map<UserEditViewModel, UserUpdateDTO>(user);
            await UserService.UpdateUser(userUpdateDTO);

            var response = Request.CreateResponse<int>(HttpStatusCode.OK, userUpdateDTO.UserID);

            // update, add UserCookie
            CookieHeaderValue cookie = Request.Headers.GetCookies(BLL.Utils.UserCookieHelper.userCookieName).FirstOrDefault();
            var updatedCookie = BLL.Utils.UserCookieHelper.UpdateUserCookie(cookie, userUpdateDTO);
            response.Headers.AddCookies(new CookieHeaderValue[] { updatedCookie });

            return response;
        }

        [HttpGet]
        [Route("isAuthenticated")]
        [AllowAnonymous]
        public IHttpActionResult GetAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

    }
}
