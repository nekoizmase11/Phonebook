using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;


namespace Phonebook.Presentation.Web.Models
{
    public class ChangedPasswordVM
    {
        [HiddenInput(DisplayValue = false)]
        public string guid { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("password", ErrorMessage = "The Password and Confirm password fields do not match.")]
        public string confirmPassword { get; set; }

        public ChangedPasswordVM(string guid, string password, string confirmPassword)
        {
            this.guid = guid;
            this.password = password;
            this.confirmPassword = confirmPassword;
        }

        public ChangedPasswordVM()
        {
        }

        public static explicit operator ChangedPasswordDto(ChangedPasswordVM enitity)
        {
            return new ChangedPasswordDto(enitity.guid, enitity.password);
        }
    }
}