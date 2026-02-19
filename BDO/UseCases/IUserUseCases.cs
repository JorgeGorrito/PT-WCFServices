using PruebaTecnica.BDO.Entities;

namespace PruebaTecnica.BDO.UseCases
{
    public interface IUserUseCases
    {
        int AddUser(User user);
        User GetUserById(int userId);
        PaginatedResult<User> GetUsersPaginated(bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);
        PaginatedResult<User> GetUsersByNamePaginated(string name, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);
        void UpdateUser(int userId, User user);
        void DeleteUser(int userId);
    }
}
