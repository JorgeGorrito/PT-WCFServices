using PruebaTecnica.BDO.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PruebaTecnica.BDO.Repositories
{
    /// <summary>
    /// Define el contrato para las operaciones de persistencia del Usuario.
    /// La implementación debe recibir una conexión abierta desde la capa de negocio.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registra un nuevo usuario y retorna el ID generado.
        /// </summary>
        int CreateUser(User userToCreate, SqlConnection connection, SqlTransaction transaction = null);

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        User GetUserByID(int userId, SqlConnection connection);

        /// <summary>
        /// Obtiene una lista paginada de usuarios activos con el total de registros.
        /// </summary>
        PaginatedResult<User> GetUsersPaginated(SqlConnection connection, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);

        /// <summary>
        /// Obtiene una lista paginada filtrada por género con el total de registros.
        /// </summary>
        PaginatedResult<User> GetUserByGenderIDPaginated(int genderId, SqlConnection connection, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);

        /// <summary>
        /// Obtiene una lista paginada filtrada por nombre con el total de registros.
        /// </summary>
        PaginatedResult<User> GetUsersByNamePaginated(string name, SqlConnection connection, bool orderIdDesc = true, int pageSize = 10, int currentPage = 1);

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        void UpdateUserByID(User userData, int userId, SqlConnection connection, SqlTransaction transaction = null);

        /// <summary>
        /// Realiza un borrado lógico (Soft Delete) del usuario.
        /// </summary>
        void RemoveUserByID(int userId, SqlConnection connection, SqlTransaction transaction = null);
    }
}
