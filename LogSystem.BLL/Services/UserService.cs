using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Utils;
using LogSystem.BLL.Interfaces;
using LogSystem.BLL.DTO.UserActionDTO;
using LogSystem.Common.Helpers;
using LogSystem.DAL.Entities;
using LogSystem.DAL.Interfaces;
using LogSystem.DAL.Repositories;
using LogSystem.Common.Enums;
using System.Threading.Tasks;

namespace LogSystem.BLL.Services
{
    public class UserService: IUserService
    {
        private IUserActionService UserActionService { get; set; }
        private IUserRepository UserRepository { get; set; }
        private AMapper AMapper { get; set; }
        private HashHelper HashHelper { get; set; }

        public UserService()
        {
            UserActionService = new UserActionService();
            UserRepository = new UserRepository();
            AMapper = new AMapper();
            HashHelper = new HashHelper(StaticSalt.Salt);
        }

        /// <summary>
        /// get UserLoginDTO by username and password
        /// </summary>
        /// <param name="userLogInDTO"></param>
        /// <returns></returns>
        public async Task<UserGetDetailDTO> LogIn(UserLogInDTO userLogInDTO)
        {
            
            // find users by UserName
            // delete after making ValidationService
            User userEntity = await UserRepository.GetByUserName(userLogInDTO.UserName);
            
            //if (userEntity != null && IsRightPassword(userLogInDTO.Password, userEntity.HashedPassword, userEntity.DynamicSalt))
            //{          
            var  userGetDetailDTO = AMapper.Mapper.Map<User, UserGetDetailDTO>(userEntity);
            //}
            //else
            //{
            //    userGetDetailDTO = null;
            //}

            // add record about signing in the user
            var userAction = new UserActionCreateDTO(userGetDetailDTO.UserID, UserActionType.LogIn);
            await UserActionService.InsertUserActionToDB(userAction);

            return userGetDetailDTO;
        }
        
        public async Task<int> SignUp(UserCreateDTO user)
        {
            string dynamicSalt = HashHelper.GetDynamicSalt(user.UserName);
            string hashedPassword = HashHelper.GetPasswordHash(user.Password, dynamicSalt);

            // Map, add fields to customer entity
            User userEntity = AMapper.Mapper.Map<UserCreateDTO, User>(user);
            userEntity.DynamicSalt = dynamicSalt;
            userEntity.HashedPassword = hashedPassword;

            // Insert the user into the database
            int newUserID = await UserRepository.Insert(userEntity);

            // add record about registration of the user
            var userAction = new UserActionCreateDTO(newUserID, UserActionType.SignUp);
            await UserActionService.InsertUserActionToDB(userAction);

            return newUserID;
            //    Database.Commit();
        }

        public async Task UpdateUser(UserUpdateDTO user)
        {
            // Map, add fields to customer entity
            User userEntity = AMapper.Mapper.Map<UserUpdateDTO, User>(user);

            var outdatedUser = await UserRepository.GetById(user.UserID);
            
            if (user.Password == null)
            {
                userEntity.HashedPassword = outdatedUser.HashedPassword;
            }
            else
            {
                string hashedPassword = HashHelper.GetPasswordHash(user.Password, outdatedUser.DynamicSalt);
                userEntity.HashedPassword = hashedPassword;
            }
                        
            // Update the customer in the database
            await UserRepository.Update(userEntity);

            // add record about editing of the user
            var userAction = new UserActionCreateDTO(userEntity.UserID, UserActionType.Edit);
            await UserActionService.InsertUserActionToDB(userAction);


            //Database.Commit();
        }

        public async Task LogOut(int userID)
        {
            var userAction = new UserActionCreateDTO(userID, UserActionType.LogOut);
            await UserActionService.InsertUserActionToDB(userAction);

        }

        public async Task<UserGetDetailDTO> GetUserByID(int id)
        {
            User userEntity = await UserRepository.GetById(id);
            var userGetDetailDTO = AMapper.Mapper.Map<User, UserGetDetailDTO>(userEntity);
            return userGetDetailDTO;
        }

        public async Task<UserGetDetailDTO> GetUserByUserName(string username)
        {
            User userEntity = await UserRepository.GetByUserName(username);
            var userGetDetailDTO = AMapper.Mapper.Map<User, UserGetDetailDTO>(userEntity);
            return userGetDetailDTO;
        }


        //public void Dispose()
        //{
        //    Database.Dispose();
        //}
    }
}
