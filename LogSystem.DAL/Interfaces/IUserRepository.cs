using LogSystem.DAL.Entities;
using System.Threading.Tasks;

namespace LogSystem.DAL.Interfaces
{
    public interface IUserRepository
    {
        //Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByUserName(string username);
        Task<int> Insert(User entity);
        Task Update(User entity);
    }
}
