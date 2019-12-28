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
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly SQLServerContext _Context;

        public ContactRepository(SQLServerContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));

            _Context = ctx;
        }

        private Contact CreateInstance(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (!reader.HasRows)
                throw new ArgumentOutOfRangeException(nameof(reader));

            return new Contact((int)reader["Id_contact"], reader["Ime"] as string, reader["Prezime"] as string, reader["Email"] as string, reader["Broj"] as string, (int)reader["Id_user"]);
        }

        //************************************************************

        public int Add(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (SqlCommand command = _Context.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Tbl_contact " +
                    "VALUES(@Ime, @Prezime, @Email, @Broj, @Id_user);" +
                    "SELECT CAST(scope_identity() AS int); ";
                    command.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = entity.Name;
                    command.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = entity.LName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    command.Parameters.Add("@Broj", SqlDbType.NVarChar).Value = entity.Number;
                    command.Parameters.Add("@Id_user", SqlDbType.Int).Value = entity.id_user;
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

        public IEnumerable<Contact> GetAll()
        {
            List<Contact> ListOfContacts = new List<Contact>();

            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "SELECT Id_contact, Ime, Prezime, Email, Broj, Id_user FROM Tbl_contact";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListOfContacts.Add(CreateInstance(reader));
                    }
                    return ListOfContacts;
                }
            }
        }

        public Contact GetById(int id)
        {
            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "SELECT Id_contact, Ime, Prezime, Email, Broj, Id_user FROM Tbl_contact " +
                "WHERE Id_contact=@id ";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return CreateInstance(reader);
                }
            }
        }

        public void Remove(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlCommand command = _Context.CreateCommand())
            {
                command.CommandText = "DELETE Tbl_contact " +
                "WHERE Id_contact=@Id";
                command.Parameters.Add("@Id", SqlDbType.Int).Value = entity.id;

                command.ExecuteNonQuery();
            }
        }

        public void Update(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                using (SqlCommand command = _Context.CreateCommand())
                {
                    command.CommandText = "UPDATE Tbl_contact " +
                    "SET Ime=@Ime, " +
                    "Prezime=@Prezime, " +
                    "Email=@Email, " +
                    "Broj=@Broj " +
                    "WHERE Id_contact=@Id";
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = entity.id;
                    command.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = entity.Name;
                    command.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = entity.LName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;
                    command.Parameters.Add("@Broj", SqlDbType.NVarChar).Value = entity.Number;

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
