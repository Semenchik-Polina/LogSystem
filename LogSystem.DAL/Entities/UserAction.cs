using LogSystem.Common.Enums;

namespace LogSystem.DAL.Entities
{
    public class UserAction
    {
        public int UserActionID { get; set; }
        // store date as string because SQLite doesn't have DateTime type
        public string Date { get; set; }
        public UserActionType Type { get; set; }

        public int FK_UserID { get; set; }
        public User User { get; set; }
    }
}
