using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.DataTransferObjects
{
    public class ChangedPasswordDto
    {
        public ChangedPasswordDto(string guid, string password)
        {
            this.guid = guid;
            this.password = password;
        }

        public string guid { get; set; }

        public string password { get; set; }
    }
}
