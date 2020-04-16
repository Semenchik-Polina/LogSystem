using LogSystem.BLL.DTO.UserActionDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem.BLL.Interfaces
{
    public interface IUserActionService
    {
        Task<IEnumerable<UserActionGetDetailDTO>> GetAllUserActions();
        Task<IEnumerable<UserActionGetDetailDTO>> GetUserActionsByDate(DateTime dateTime);
        Task InsertUserActionToDB(UserActionCreateDTO userAction);
    }
}
