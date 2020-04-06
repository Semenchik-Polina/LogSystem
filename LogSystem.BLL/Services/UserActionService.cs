using LogSystem.BLL.DTO.UserActionDTO;
using LogSystem.BLL.Utils;
using LogSystem.BLL.Interfaces;
using LogSystem.Common.Helpers;
using LogSystem.DAL.Entities;
using System.Threading.Tasks;
using LogSystem.DAL.Interfaces;
using LogSystem.DAL.Repositories;
using System.Collections.Generic;
using LogSystem.Common.Enums;

namespace LogSystem.BLL.Services
{
    public class UserActionService: IUserActionService
    {
        private IUserActionRepository UserActionRepository { get; set; }
        private AMapper AMapper { get; set; }

        public UserActionService()
        {
            UserActionRepository = new UserActionRepository();
            AMapper = new AMapper();
        }

        public async Task<IEnumerable<UserActionGetDetailDTO>> GetAllUserActions()
        {
            IEnumerable<UserAction> userActions = await UserActionRepository.GetAll();
            var UserActionDTOs = AMapper.Mapper.Map<IEnumerable<UserAction>, IEnumerable<UserActionGetDetailDTO>>(userActions);
            return UserActionDTOs;
        }

        public async Task<IEnumerable<UserActionGetDetailDTO>> GetUserActionsByType(UserActionType type)
        {
            IEnumerable<UserAction> userActions = await UserActionRepository.GetByType(type);
            var UserActionDTOs = AMapper.Mapper.Map<IEnumerable<UserAction>, IEnumerable<UserActionGetDetailDTO>>(userActions);
            return UserActionDTOs;
        }

        public async Task InsertUserActionToDB(UserActionCreateDTO userAction)
        {
            // Map, add fields to customer entity
            UserAction userActionEntity = AMapper.Mapper.Map<UserActionCreateDTO, UserAction>(userAction);   
            // Insert a customer into the database
            await UserActionRepository.Insert(userActionEntity);
        }
    }
}
