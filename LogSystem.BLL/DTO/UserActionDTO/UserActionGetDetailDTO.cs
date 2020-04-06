using LogSystem.Common.Enums;
using System;

namespace LogSystem.BLL.DTO.UserActionDTO
{
    public class UserActionGetDetailDTO
    {
        public int UserActionID { get; set; }
        public DateTime Date { get; set; }
        public UserActionType Type { get; set; }

        public int FK_UserID { get; set; }
        public UserGetDetailDTO User { get; set; }
    }
}
