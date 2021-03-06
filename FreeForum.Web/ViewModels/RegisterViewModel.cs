﻿using System.ComponentModel.DataAnnotations;

namespace FreeForum.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        public string PasswordConfirm { get; set; }
    }
}
