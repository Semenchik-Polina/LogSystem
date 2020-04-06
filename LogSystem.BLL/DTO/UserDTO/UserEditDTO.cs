using LogSystem.Common.Enums;

namespace LogSystem.BLL.DTO.UserDTO
{
    public class UserEditDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}
