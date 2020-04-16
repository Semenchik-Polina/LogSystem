using LogSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem.DAL.Interfaces
{
    public interface IUserActionRepository
    {
        Task<IEnumerable<UserAction>> GetAll();
        Task<IEnumerable<UserAction>> GetByDate(string date);
        Task Insert(UserAction entity);
    }
}
