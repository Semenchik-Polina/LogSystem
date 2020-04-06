using LogSystem.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LogSystem.PL.Models
{
    public class UserActionViewModel
    {
        public int UserActionID { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public DateTime Date { get; set; }

        public UserActionType Type { get; set; }
    }
}