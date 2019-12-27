using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.Models
{
    public class ChangePasswordRequest
    {
        public string Id_Request { get; set; }
        public int Id_user { get; set; }
    }
}
