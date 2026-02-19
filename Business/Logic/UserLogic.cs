using PruebaTecnica.BDO.Entities;
using PruebaTecnica.BDO.Repositories;
using PruebaTecnica.DataAccess.Repositories;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using PruebaTecnica.BDO.UseCases;

namespace PruebaTecnica.Business.Logic
{
    public class UserLogic : IUserUseCases
    {
        private readonly IUserRepository _userRepository;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DigitalBankDB"].ConnectionString;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddUser(User user)
        {
            int userCreatedID = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                userCreatedID = _userRepository.CreateUser(user, conn);
            }
            return userCreatedID;
        }

        public User GetUserById(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                return _userRepository.GetUserByID(userId, conn);
            }
        }

        public PaginatedResult<User> GetUsersPaginated(bool orderIdDesc = true, int pageSize = 10, int currentPage = 1)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                return _userRepository.GetUsersPaginated(conn, orderIdDesc, pageSize, currentPage);
            }
        }

        public PaginatedResult<User> GetUsersByNamePaginated(string name, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                return _userRepository.GetUsersByNamePaginated(name, conn, orderIdDesc, pageSize, currentPage);
            }
        }

        public void UpdateUser(int userId, User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                _userRepository.UpdateUserByID(user, userId, conn);
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                _userRepository.RemoveUserByID(userId, conn);
            }
        }
    }
}