using Phonebook.Bussines.Contracts.Models;
using Phonebook.Bussines.Contracts.RepositoryInterfaces;
using Phonebook.Data.SQLServerDbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Phonebook.Data.SQLServerExceptions;
using Phonebook.Data.SQLServerExceptions.SQLServerExceptionsEnums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Repositories
{
    public class ChangePasswordRequestRepository : IChangePasswordRequestRepository
    {
        private readonly SQLServerContext _Context;

        public ChangePasswordRequestRepository(SQLServerContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));

            _Context = ctx;
        }

        public void Add(ChangePasswordRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (SqlCommand command = _Context.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Tbl_resetPassword " +
                    "VALUES(@Id_request, @Id_user);";
                    command.Parameters.Add("@Id_request", SqlDbType.NVarChar).Value = entity.Id_Request;
                    command.Parameters.Add("@Id_user", SqlDbType.Int).Value = entity.Id_user;
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == (int)SQLExceptions.InvalidData)
            {
                throw new SQLServerInvalidDataException("Invalid data provided (null to non null column)!", ex);
            }
        }

        public int GetUserIdByGuid(string guid)
        {
            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "SELECT Id_user FROM Tbl_resetPassword " +
                "WHERE Id_request=@guid ";
                command.Parameters.Add("@guid", SqlDbType.NVarChar).Value = guid;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return (int)reader["Id_user"];
                }
            }
        }

        public void Remove(ChangePasswordRequest entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "DELETE Tbl_resetPassword " +
                "WHERE Id_request=@Guid";
                command.Parameters.Add("@Guid", SqlDbType.NVarChar).Value = entity.Id_Request;

                command.ExecuteNonQuery();
            }
        }
    }
}
