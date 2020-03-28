using LogSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem.DAL.Entities
{
    public class UserAction
    {
        public int UserActionID { get; set; }
        public int UserID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserActionType Type { get; set; }
    }
}
