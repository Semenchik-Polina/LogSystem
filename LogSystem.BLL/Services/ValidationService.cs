using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.Common;
using LogSystem.Common.Helpers;
using System.Threading.Tasks;

namespace LogSystem.BLL.Services
{
    public class ValidationService: IValidationService
    {
        private HashHelper HashHelper { get; set; }
        private UserService UserService { get; set; }
        private ErrorHelper ErrorHelper { get; set; }

        public ValidationService()
        {
            HashHelper = new HashHelper(StaticSalt.Salt);
            UserService = new UserService();
            ErrorHelper = new ErrorHelper();
        }

        public async Task<ErrorModel> ValidateLogInUser(string username, string password)
        {
            var error = await IsAccountExists(username, password);
            return error;
        }

        public async Task<ErrorModel> IsAccountExists(string username, string password)
        {
            UserGetDetailDTO userGetDetailDTO = await UserService.GetUserByUserName(username);

            // if user with the userName not exists or
            // if user with the userName exists but password isn't right
            if (userGetDetailDTO == null || !IsRightPassword(password, userGetDetailDTO.HashedPassword, userGetDetailDTO.DynamicSalt))
            {
                return ErrorHelper.AccountNotFound;
            }
            return null;
        }

        private bool IsRightPassword(string password, string storedPassword, string dynamicSalt)
        {
            string hashedPassword = HashHelper.GetPasswordHash(password, dynamicSalt);
            return (hashedPassword == storedPassword);
        }

    }
}
