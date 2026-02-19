using PruebaTecnica.BDO.Entities;
using PruebaTecnica.BDO.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PruebaTecnica.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string SP_MANAGE_USER = "sp_ManageUser";

        public int CreateUser(User userToCreate, SqlConnection connection, SqlTransaction transaction = null)
        {
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (transaction != null)
                    cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@Action", "CREATE");
                cmd.Parameters.AddWithValue("@Name", userToCreate.Name);
                cmd.Parameters.AddWithValue("@BirthDate", userToCreate.BirthDate);
                cmd.Parameters.AddWithValue("@Gender", userToCreate.Gender);

                EnsureConnectionOpen(connection);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public User GetUserByID(int userId, SqlConnection connection)
        {
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GET_BY_ID");
                cmd.Parameters.AddWithValue("@UserId", userId);

                EnsureConnectionOpen(connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }
            }
            return null;
        }

        public PaginatedResult<User> GetUsersPaginated(SqlConnection connection, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1)
        {
            var result = new PaginatedResult<User>();
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GET_PAGINATED");
                cmd.Parameters.AddWithValue("@OrderDesc", orderIdDesc);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@CurrentPage", currentPage);

                EnsureConnectionOpen(connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Items.Add(MapUser(reader));
                        if (result.TotalCount == 0)
                        {
                            result.TotalCount = Convert.ToInt32(reader["TotalCount"]);
                        }
                    }
                }
            }
            return result;
        }

        public PaginatedResult<User> GetUserByGenderIDPaginated(
            int genderId, 
            SqlConnection connection, 
            bool orderIdDesc = true, 
            int pageSize = 10, 
            int currentPage = 1
            ){
            char genderChar = (genderId == 1) ? 'M' : 'F';

            var result = new PaginatedResult<User>();
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GET_BY_GENDER_PAGINATED");
                cmd.Parameters.AddWithValue("@Gender", genderChar);
                cmd.Parameters.AddWithValue("@OrderDesc", orderIdDesc);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@CurrentPage", currentPage);

                EnsureConnectionOpen(connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Items.Add(MapUser(reader));
                        if (result.TotalCount == 0)
                        {
                            result.TotalCount = Convert.ToInt32(reader["TotalCount"]);
                        }
                    }
                }
            }
            return result;
        }

        public PaginatedResult<User> GetUsersByNamePaginated(
            string name,
            SqlConnection connection,
            bool orderIdDesc = true,
            int pageSize = 10,
            int currentPage = 1
            )
        {
            var result = new PaginatedResult<User>();
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GET_BY_NAME_PAGINATED");
                cmd.Parameters.AddWithValue("@Name", name ?? string.Empty);
                cmd.Parameters.AddWithValue("@OrderDesc", orderIdDesc);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@CurrentPage", currentPage);

                EnsureConnectionOpen(connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Items.Add(MapUser(reader));
                        if (result.TotalCount == 0)
                        {
                            result.TotalCount = Convert.ToInt32(reader["TotalCount"]);
                        }
                    }
                }
            }
            return result;
        }


        public void UpdateUserByID(User userData, int userId, SqlConnection connection, SqlTransaction transaction = null)
        {
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (transaction != null)
                    cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Name", userData.Name);
                cmd.Parameters.AddWithValue("@BirthDate", userData.BirthDate);
                cmd.Parameters.AddWithValue("@Gender", userData.Gender);

                EnsureConnectionOpen(connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveUserByID(int userId, SqlConnection connection, SqlTransaction transaction = null)
        {
            using (var cmd = new SqlCommand(SP_MANAGE_USER, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (transaction != null)
                    cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@Action", "REMOVE");
                cmd.Parameters.AddWithValue("@UserId", userId);

                EnsureConnectionOpen(connection);
                cmd.ExecuteNonQuery();
            }
        }

        #region Helpers

        private void EnsureConnectionOpen(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                ID = Convert.ToInt32(reader["UserId"]),
                Name = reader["Name"].ToString(),
                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                Gender = Convert.ToChar(reader["Gender"])
            };
        }

        #endregion
    }
}