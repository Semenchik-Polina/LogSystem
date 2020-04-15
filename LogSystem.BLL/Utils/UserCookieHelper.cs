using LogSystem.BLL.DTO.UserDTO;
using LogSystem.Common.Enums;
using System;
using System.Net.Http.Headers;
using System.Collections.Specialized;


namespace LogSystem.BLL.Utils
{
    public static class UserCookieHelper
    {
        public const string userCookieName = "User";

        // create userCookie for UserGetDetailDTO
        public static CookieHeaderValue CreateUserCookie(UserGetDetailDTO user)
        {
            var httpCookie = CreateCookie(user.UserID, user.Type);
            return httpCookie;
        }

        // create userCookie by UserID, UserType
        private static CookieHeaderValue CreateCookie(int userID, UserType type)
        {
            var vals = new NameValueCollection
            {
                ["UserID"] = userID.ToString(),
                ["UserType"] = type.ToString()
            };
            var cookie = new CookieHeaderValue(userCookieName, vals)
            {
                Expires = DateTime.Now.AddDays(1),
                Path = "/"
            };
            return cookie;
        }

        public static CookieHeaderValue UpdateUserCookie(CookieHeaderValue httpCookie, UserUpdateDTO user)
        {
            DeleteUserCookie(httpCookie);
            var cookie = CreateCookie(user.UserID, user.Type);
            return cookie;
        }

        public static void DeleteUserCookie(CookieHeaderValue httpCookie)
        {
            httpCookie.Expires = DateTime.Now.AddDays(-1);
        }

    }
}
