using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phonebook.Presentation.Web.Models
{
    public class EmailVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "From:")]
        public string From { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "To:")]
        public string To { get; set; }

        [Required]
        [Display(Name = "Text of Email:")]
        public string Body { get; set; }

        public static explicit operator EmailDto(EmailVM email)
        {
            return new EmailDto(email.From, email.To, email.Body);
        }
    }
}