using LogSystem.Common.Enums;
using LogSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem.DAL.Interfaces
{
    public interface IUserActionRepository
    {
        Task<IEnumerable<UserAction>> GetAll();
        Task<IEnumerable<UserAction>> GetByDate(DateTime date);
        Task<IEnumerable<UserAction>> GetByType(UserActionType type);
        Task<IEnumerable<UserAction>> GetByUserID(int userID);
        Task Insert(UserAction entity);
    }
}
