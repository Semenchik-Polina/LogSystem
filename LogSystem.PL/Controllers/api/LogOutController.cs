using LogSystem.BLL.Interfaces;
using LogSystem.BLL.Utils;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace LogSystem.PL.Controllers.api
{
    [Authorize]
    public class LogOutController : ApiController
    {
        private IUserService UserService { get; set; }

        public LogOutController(IUserService userService)
        {
            UserService = userService;
        }

        // POST: api/LogOut
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync()
        {
            try
            {
                // log out using UserID from UserCookie 
                var userCookie = Request.Headers.GetCookies(UserCookieHelper.userCookieName).FirstOrDefault();
                int id = Int32.Parse(userCookie[UserCookieHelper.userCookieName]["UserID"]);
                await UserService.LogOut(id);


                // reset expiration date (remove cookie)
                FormsAuthentication.SignOut();
                var authCookie = Request.Headers.GetCookies(FormsAuthentication.FormsCookieName).FirstOrDefault();
                if (authCookie != null)
                {
                    authCookie.Expires = DateTime.Now.AddYears(-1);
                }
                UserCookieHelper.DeleteUserCookie(userCookie);

                // send response with expired cookies
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.AddCookies(new CookieHeaderValue[] { userCookie });

                return response;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        
    }
}
