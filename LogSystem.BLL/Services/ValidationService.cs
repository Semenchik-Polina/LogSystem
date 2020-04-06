using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.Common;
using LogSystem.Common.Helpers;
using System.Threading.Tasks;

namespace LogSystem.BLL.Services
{
    public class ValidationService: IValidationService
    {
        //private UnitOfWork Database { get; set; }
        private HashHelper HashHelper { get; set; }
        private UserService UserService { get; set; }
        private ErrorHelper ErrorHelper { get; set; }

        public ValidationService()
        {
            //Database = uow;
            HashHelper = new HashHelper(StaticSalt.Salt);
            UserService = new UserService();
            ErrorHelper = new ErrorHelper();
        }

        public async Task<ErrorModel> ValidateLogInUser(string username, string password)
        {
            var error = await IsAccountExists(username, password);
            return error;
        }

        public async Task<ErrorModel> ValidateSignUpUser(UserCreateDTO user)
        {
            ErrorModel error = null;
            if (!(await IsUserNameAvailable(user.UserName)))
            {
                error = ErrorHelper.UserNameIsTaken;
            }            
            return error;
        }

        public async Task<bool> IsUserNameAvailable(string username)
        {
            UserGetDetailDTO userGetDetailDTO = await UserService.GetUserByUserName(username);
            if (userGetDetailDTO != null)
            {
                return false;
            }
            return true;
        }

        public async Task<ErrorModel> IsAccountExists(string username, string password)
        {
            UserGetDetailDTO userGetDetailDTO = await UserService.GetUserByUserName(username);

            // if user with the email exists but password isn't right
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
