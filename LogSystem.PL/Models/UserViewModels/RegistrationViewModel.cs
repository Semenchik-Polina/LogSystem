﻿using LogSystem.Common.Enums;

namespace LogSystem.PL.Models.UserViewModels
{
    public class RegistrationViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}