using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phonebook.Presentation.Web.Models
{
    public class UserVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string password { get; set; }

        [Required]
        [Compare("password")]
        [Display(Name = "Confirm password:")]
        public string confirmPassword { get; set; }
    }
}