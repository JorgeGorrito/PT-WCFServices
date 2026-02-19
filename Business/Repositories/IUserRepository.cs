using PruebaTecnica.Business.Entities;
using System.Collections.Generic;

namespace PruebaTecnica.Business.Repositories
{
    public interface IUserRepository
    {
        int CreateUser(User userToCreate);
        User GetUserByID(int userId);
        List<User> GetUsersPaginated(bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);
        List<User> GetUserByGenderIDPaginated(int genderId, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);
        void updateUserByID(User userData, int userId);
        void removeUserByID(int userId);
    }
}
