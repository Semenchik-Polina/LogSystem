using LogSystem.Common.Enums;
using System;

namespace LogSystem.BLL.DTO.UserDTO
{
    public class UserCreateDTO
    {
        public UserCreateDTO()
        {
            RegistrationDate = DateTime.Now;
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserType Type { get; set; }

    }
}
