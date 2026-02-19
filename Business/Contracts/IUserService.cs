using PruebaTecnica.Business.DataContracts.Requests;
using PruebaTecnica.Business.DataContracts.Responses;
using System.ServiceModel;

namespace PruebaTecnica.Business.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        BaseResponse<AddUserResponse> AddUser(AddUserContract contract);

        [OperationContract]
        BaseResponse<UserDto> GetUserById(GetUserByIdContract contract);

        [OperationContract]
        BaseResponse<GetUsersPaginatedResponse> GetUsersPaginated(GetUsersPaginatedContract contract);

        [OperationContract]
        BaseResponse<GetUsersPaginatedResponse> GetUsersByNamePaginated(GetUsersByNamePaginatedContract contract);

        [OperationContract]
        BaseResponse<UpdateUserResponse> UpdateUser(UpdateUserContract contract);

        [OperationContract]
        BaseResponse<DeleteUserResponse> DeleteUser(DeleteUserContract contract);
    }
}
