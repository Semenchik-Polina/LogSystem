using LogSystem.Common.Enums;

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
        // store date as string because SQLite doesn't have DateTime type
        public string RegistrationDate { get; set; }
        public string DynamicSalt { get; set; }
    }
}
