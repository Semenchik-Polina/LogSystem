using LogSystem.Common.Enums;
using System;

namespace LogSystem.BLL.DTO.UserDTO
{
    public class UserGetDetailDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string DynamicSalt { get; set; }
    }
}
