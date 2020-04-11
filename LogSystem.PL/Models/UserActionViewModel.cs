using LogSystem.Common.Enums;
using System;

namespace LogSystem.PL.Models
{
    public class UserActionViewModel
    {
        public int UserActionID { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public UserActionType Type { get; set; }
    }
}