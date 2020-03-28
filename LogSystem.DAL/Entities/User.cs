using LogSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem.DAL.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string RegistrationDate { get; set; }
        public string DynamicSalt { get; set; }
    }
}
