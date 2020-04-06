using LogSystem.Common.Enums;
using System;

namespace LogSystem.BLL.DTO.UserActionDTO
{
    public class UserActionCreateDTO
    {
        public UserActionCreateDTO(int userID, UserActionType type)
        {
            FK_UserID = userID;
            Type = type;
            Date = DateTime.Now;
        }

        public int UserActionID { get; set; }
        public DateTime Date { get; set; }
        public UserActionType Type { get; set; }
        public int FK_UserID { get; set; }
    }
}
