using LogSystem.BLL.DTO.UserDTO;
using LogSystem.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetDetailDTO>> GetAllUsersByType(UserType type);
        Task<UserGetDetailDTO> GetUserByUserName(string username);
        Task<UserGetDetailDTO> GetUserByID(int id);
        Task<UserGetDetailDTO> LogIn(UserLogInDTO userLogInDTO);
        Task<int> SignUp(UserCreateDTO user);
        Task UpdateUser(UserUpdateDTO user);
        Task LogOut(int userID);
    }
}
