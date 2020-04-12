using LogSystem.BLL.DTO.UserDTO;
using LogSystem.Common.Enums;
using System;
using System.Text;
using System.Web;

namespace LogSystem.BLL.Utils
{
    public static class UserCookieHelper
    {
        public const string userCookieName = "User";

        public static HttpCookie CreateAuthCookie(UserGetDetailDTO user)
        {
            HttpCookie cookie = new HttpCookie("Authorization");

            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(user.UserName + ":" + user.HashedPassword));
            cookie.Value = "Basic " + encoded;

            return cookie;
        }

        public static HttpCookie CreateUserCookie(UserGetDetailDTO user)
        {
            var httpCookie = CreateCookie(user.UserID, user.Type);
            return httpCookie;
        }

        private static HttpCookie CreateCookie(int userID, UserType type)
        {
            HttpCookie cookie = new HttpCookie(userCookieName);
            cookie["UserID"] = userID.ToString();
            cookie["UserType"] = type.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            return cookie;
        }

        public static HttpCookie UpdateUserCookie(HttpCookie httpCookie, UserUpdateDTO user)
        {
            DeleteUserCookie(httpCookie);
            var cookie = CreateCookie(user.UserID, user.Type);
            return cookie;
        }

        public static void DeleteUserCookie(HttpCookie httpCookie)
        {
            httpCookie.Expires = DateTime.Now.AddDays(-1);
        }
    }
}
