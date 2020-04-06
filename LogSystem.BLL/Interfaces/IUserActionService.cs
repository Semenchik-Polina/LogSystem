using LogSystem.BLL.DTO.UserActionDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSystem.BLL.Interfaces
{
    public interface IUserActionService
    {
        Task<IEnumerable<UserActionGetDetailDTO>> GetAllUserActions();
        Task InsertUserActionToDB(UserActionCreateDTO userAction);
    }
}
