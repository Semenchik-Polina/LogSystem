using LogSystem.BLL.DTO.UserDTO;
using System.Threading.Tasks;

namespace LogSystem.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserGetDetailDTO> LogIn(UserLogInDTO userLogInDTO);
        Task<int> SignUp(UserCreateDTO user);
        Task UpdateUser(UserUpdateDTO user);
        Task LogOut(int userID);
        Task<UserGetDetailDTO> GetUserByUserName(string username);
        Task<UserGetDetailDTO> GetUserByID(int id);
    }
}
