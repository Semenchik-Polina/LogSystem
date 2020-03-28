using LogSystem.Common.Enums;
using System;

namespace LogSystem.DAL.Entities
{
    public class UserAction
    {
        public int UserActionID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public UserActionType Type { get; set; }
    }
}
