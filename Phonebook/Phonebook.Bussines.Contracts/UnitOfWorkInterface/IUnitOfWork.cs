using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.UnitOfWorkInterface
{
    public interface IUnitOfWork
    {
        SqlTransaction CurrentTransaction { get; }
    }
}
