using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;


namespace Phonebook.Presentation.Web.Models
{
    public class RegisterVM
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("password", ErrorMessage = "The Password and Confirm password fields do not match.")]
        public string confirmPassword { get; set; }
    }
}