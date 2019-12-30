using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phonebook.Presentation.Web.Models
{
    public class ContactVM
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }
        [Required]
        [Display(Name = "Name:")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Last name:")]
        public string LName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone number:")]
        public string Number { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int id_user { get; set; }

        public ContactVM() { }

        public ContactVM(int id, string name, string lName, string email, string number, int id_user)
        {
            this.id = id;
            Name = name;
            LName = lName;
            Email = email;
            Number = number;
            this.id_user = id_user;
        }

        public static explicit operator ContactVM(ContactDto contact)
        {
            return new ContactVM(contact.id, contact.Name, contact.LName, contact.Email, contact.Number, contact.id_user);
        }

        public static explicit operator ContactDto(ContactVM contact)
        {
            return new ContactDto(contact.id, contact.Name, contact.LName, contact.Email, contact.Number, contact.id_user);
        }
    }
}