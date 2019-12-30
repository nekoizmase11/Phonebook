using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phonebook.Presentation.Web.Models
{
    public class SearchVM
    {
        public SearchVM(bool? ime, bool? prezime, bool? email, bool? broj, string searchTextBox)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.email = email;
            this.broj = broj;
            SearchTextBox = searchTextBox;
        }

        public bool? ime { get; set; }
        public bool? prezime { get; set; }
        public bool? email { get; set; }
        public bool? broj { get; set; }
        public string SearchTextBox { get; set; }

        public SearchVM()
        {
        }
    }
}