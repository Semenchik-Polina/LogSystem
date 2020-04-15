using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.Utils;
using LogSystem.Common.Helpers;
using LogSystem.PL.Models.UserViewModels;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace LogSystem.PL.Controllers.api
{
    [AllowAnonymous]
    public class LogInController : ApiController
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
        
        // POST: api/LogIn
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody]AuthorizationViewModel authVM)
        {
            // check if the logIn user exists
            var error = await ValidationService.ValidateLogInUser(authVM.UserName, authVM.Password);
            if (error != null)
            {
                ModelState.AddModelError("Username", error.description);
            }
            if (!ModelState.IsValid)
            {
                // if there is no user with authVM parameters return empty result  
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            // logging in
            UserLogInDTO userLogInDTO = AMapper.Mapper.Map<AuthorizationViewModel, UserLogInDTO>(authVM);
            UserGetDetailDTO userGetDTO = await UserService.LogIn(userLogInDTO);
                    
            // set auth and userCookie
            FormsAuthentication.SetAuthCookie(userGetDTO.UserName, true);
            var response = Request.CreateResponse<int>(HttpStatusCode.OK, userGetDTO.UserID);
            var cookie = UserCookieHelper.CreateUserCookie(userGetDTO);
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                        
            return response;
        }

    }
}
