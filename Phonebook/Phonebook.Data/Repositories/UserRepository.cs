using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Bussines.Contracts.Models;
using Phonebook.Bussines.Contracts.RepositoryInterfaces;
using Phonebook.Data.SQLServerDbContext;
using Phonebook.Data.SQLServerExceptions;
using Phonebook.Data.SQLServerExceptions.SQLServerExceptionsEnums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLServerContext _Context;

        public UserRepository(SQLServerContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));

            _Context = ctx;
        }

        private User CreateInstance(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (!reader.HasRows)
                throw new ArgumentOutOfRangeException(nameof(reader));

            return new User((int)reader["Id_user"], reader["Email"] as string, reader["Password"] as string);
        }

        //*************************************************

        public int Add(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (SqlCommand command = _Context.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Tbl_user " +
                    "VALUES(@Email, @Password);" +
                    "SELECT CAST(scope_identity() AS int); ";
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.password;
                    return entity.id = (int)command.ExecuteScalar();
                }
            }
            catch (SqlException ex) when (ex.Number == (int)SQLExceptions.DuplicateData)
            {
                throw new SQLServerDuplcateDataException("Duplicate email!", ex);
            }
            catch (SqlException ex) when (ex.Number == (int)SQLExceptions.InvalidData)
            {
                throw new SQLServerInvalidDataException("Invalid data provided (null to non null column)!", ex);
            }
        }

        public User GetById(int id)
        {
            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "SELECT Id_user, Email, Password FROM Tbl_user " +
                "WHERE Id_user=@id ";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return CreateInstance(reader);
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            List<User> Users = new List<User>();

            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "SELECT Id_user, Email, Password FROM Tbl_user";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Users.Add(CreateInstance(reader));
                    }
                    return Users;
                }
            }
        }

        public void Remove(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "DELETE Tbl_user " +
                "WHERE Id=@Id";
                command.Parameters.Add("@Id", SqlDbType.Int).Value = entity.id;

                command.ExecuteNonQuery();
            }
        }

        public User GetByEmail(string email)
        {
            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "SELECT Id_user, Email, Password FROM Tbl_user " +
                "WHERE Email=@email ";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
                        return CreateInstance(reader);
                    }
                    else
                    {
                        return null;
                    }

                }
            }
        }

        public void UpdateUser(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (SqlCommand command = _Context.CreateCommand())
                {
                    command.CommandText = "UPDATE Tbl_user " +
                    "SET Email=@Email, " +
                    "Password=@Password " +
                    "WHERE Id_user=@Id";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = entity.id;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = entity.password;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == (int)SQLExceptions.DuplicateData)
            {
                throw new SQLServerDuplcateDataException("Duplicate email!", ex);
            }
            catch (SqlException ex) when (ex.Number == (int)SQLExceptions.InvalidData)
            {
                throw new SQLServerInvalidDataException("Invalid data provided (null to non null column)!", ex);
            }
        }
    }
}
