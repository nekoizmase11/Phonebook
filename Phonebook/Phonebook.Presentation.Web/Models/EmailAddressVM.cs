using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phonebook.Presentation.Web.Models
{
    public class EmailAddressVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string email { get; set; }
    }
}