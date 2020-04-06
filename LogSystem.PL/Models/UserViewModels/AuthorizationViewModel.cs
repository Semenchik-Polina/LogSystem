using System.ComponentModel.DataAnnotations;

namespace LogSystem.PL.Models.UserViewModels
{
    public class AuthorizationViewModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}