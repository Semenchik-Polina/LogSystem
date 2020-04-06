using LogSystem.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace LogSystem.PL.Models.UserViewModels
{
    public class RegistrationViewModel
    {
        [System.Web.Mvc.Remote("IsUserNameAvailable", "SignUp",
            ErrorMessage = "User with this username already exists", HttpMethod = "POST")]
        [Required(ErrorMessage = "Please enter your UserName")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [RegularExpression(@"^[A-Za-z,.'-]+$",
           ErrorMessage = "Inalid first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Za-z,.'-]+$",
           ErrorMessage = "Inalid last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        // Minimum 6 characters, at least one letter and one number: 
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
            ErrorMessage = "Password should contain minimum 6 characters, at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Compare("Password", ErrorMessage = "The fields Password and PasswordConfirmation should be equal")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "The Email address is not valid")]
        public string Email { get; set; }

        public UserType Type { get; set; }
    }
}