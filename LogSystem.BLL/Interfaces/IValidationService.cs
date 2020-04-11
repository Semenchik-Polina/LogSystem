using LogSystem.BLL.DTO.UserDTO;
using LogSystem.Common;
using System.Threading.Tasks;

namespace LogSystem.BLL.Interfaces
{
    public interface IValidationService
    {
        Task<ErrorModel> ValidateLogInUser(string username, string password);
        Task<ErrorModel> ValidateSignUpUser(UserCreateDTO user);
        Task<bool> IsUserNameTaken(string username);
        Task<ErrorModel> IsAccountExists(string username, string password);
    }
}
