using LogSystem.BLL.DTO.UserActionDTO;
using LogSystem.BLL.Utils;
using LogSystem.BLL.Interfaces;
using LogSystem.DAL.Entities;
using System.Threading.Tasks;
using LogSystem.DAL.Interfaces;
using LogSystem.DAL.Repositories;
using System.Collections.Generic;
using System;

namespace LogSystem.BLL.Services
{
    public class UserActionService: IUserActionService
    {
        private const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
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

        public async Task<IEnumerable<UserActionGetDetailDTO>> GetUserActionsByDate(DateTime dateTime)
        {
            // convert date to string because SQLite doesn't have DateTime type
            string dateTimeStr = dateTime.Date.ToString(dateTimeFormat);
            // according to string format of dateTime in SQLite first part of dateTimeStr will be "yyyy-MM-dd"
            string dateStr = dateTimeStr.Split(' ')[0];
            IEnumerable<UserAction> userActions = await UserActionRepository.GetByDate(dateStr);
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
