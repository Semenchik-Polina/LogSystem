using LogSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem.BLL.DTO.UserActionDTO
{
    public class UserActionReportDTO
    {
        public int UserActionID { get; set; }
        public DateTime Date { get; set; }
        public UserActionType Type { get; set; }
        public string UserName { get; set; }
    }
}
