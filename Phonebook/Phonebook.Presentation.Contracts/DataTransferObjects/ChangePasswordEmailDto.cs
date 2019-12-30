using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.DataTransferObjects
{
    public class ChangePasswordEmailDto
    {
        public string emailTo { get; set; }
        public string guid { get; set; }
    }
}
