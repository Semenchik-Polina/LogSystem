using System.Collections.Generic;

namespace LogSystem.Common.Helpers
{
    public class ErrorHelper
    {
        public ErrorModel UserNameIsTaken
        {
            get => ErrorDictionary["UserNameIsTaken"];
        }
        
        public ErrorModel AccountNotFound
        {
            get => ErrorDictionary["AccountNotFound"];
        }

        private readonly Dictionary<string, ErrorModel> ErrorDictionary = new Dictionary<string, ErrorModel>
        {
            {"UserNameIsTaken", new ErrorModel("UserNameIsTaken", "User with this username already exists")},
            {"AccountNotFound",  new ErrorModel("AccountNotFound", "No such account. Please, reenter your username or password")},
        };
    }
}
