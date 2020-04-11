using LogSystem.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogSystem.PL.Filters
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] allowedTypes = new string[] { };

        public UserAuthorizeAttribute()
        { }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/LogIn/LogIn");
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Roles))
            {
                allowedTypes = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedTypes.Length; i++)
                {
                    allowedTypes[i] = allowedTypes[i].Trim();
                }
            }
            var userCookie = httpContext.Request.Cookies[UserCookieHelper.userCookieName];
            return httpContext.Request.IsAuthenticated && Type(httpContext) && userCookie != null;
        }

        private bool Type(HttpContextBase httpContext)
        {
            if (allowedTypes.Length > 0)
            {
                for (int i = 0; i < allowedTypes.Length; i++)
                {
                    if (Array.IndexOf(allowedTypes, httpContext.Request.Cookies[UserCookieHelper.userCookieName]["UserType"]) > -1)
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}