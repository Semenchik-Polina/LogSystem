using LogSystem.Common.Enums;
using LogSystem.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllByUserType(UserType type);
        Task<User> GetById(int id);
        Task<User> GetByUserName(string username);
        Task<int> Insert(User entity);
        Task Update(User entity);
    }
}
