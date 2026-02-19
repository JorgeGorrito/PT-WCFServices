using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using PruebaTecnica.Business.Contracts;
using PruebaTecnica.Business.DataContracts.Requests;
using PruebaTecnica.Business.DataContracts.Responses;
using PruebaTecnica.BDO.Entities;
using PruebaTecnica.BDO.Enums;
using PruebaTecnica.BDO.UseCases;

namespace PruebaTecnica.Business.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper { get; set; }
        private IUserUseCases _userUseCases { get; set; }

        public UserService(IMapper mapper, IUserUseCases userUseCases)
        {
            _mapper = mapper;
            _userUseCases = userUseCases;
        }

        public BaseResponse<AddUserResponse> AddUser(AddUserContract contract)
        {
            return ServiceHandler.Handle(
                nameof(AddUser),
                () =>
                {
                    if (contract is null)
                        return Response.Failure<AddUserResponse>(
                            "Algo salio mal al procesar la solicitud.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El request no debe ser nulo.",
                            }
                        );

                    User user;
                    try
                    {
                        user = _mapper.Map<User>(contract);
                    }
                    catch(AutoMapperMappingException exception)
                    {
                        throw exception.InnerException;
                    }

                    int userId = _userUseCases.AddUser(user);

                    AddUserResponse result = new AddUserResponse()
                    {
                        UserAddedID = userId,
                    };

                    return Response.Success(
                        message: "Se ha agregado el usuario al banco de datos.",
                        code: StatusCode.Created,
                        result: result
                    );
                }
            );
        }

        public BaseResponse<UserDto> GetUserById(GetUserByIdContract contract)
        {
            return ServiceHandler.Handle(
                nameof(GetUserById),
                () =>
                {
                    if (contract is null)
                        return Response.Failure<UserDto>(
                            "Algo salio mal al procesar la solicitud.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El request no debe ser nulo.",
                            }
                        );

                    if (contract.UserId <= 0)
                        return Response.Failure<UserDto>(
                            "Identificador de usuario inválido.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El ID de usuario debe ser mayor a 0.",
                            }
                        );

                    User user = _userUseCases.GetUserById(contract.UserId);

                    if (user == null)
                        return Response.Failure<UserDto>(
                            "Usuario no encontrado.",
                            StatusCode.NotFound,
                            new List<string>
                            {
                                $"No se encontró un usuario con el ID {contract.UserId}.",
                            }
                        );

                    UserDto userDto = _mapper.Map<UserDto>(user);

                    return Response.Success(
                        message: "Usuario obtenido exitosamente.",
                        code: StatusCode.Success,
                        result: userDto
                    );
                }
            );
        }

        public BaseResponse<GetUsersPaginatedResponse> GetUsersPaginated(GetUsersPaginatedContract contract)
        {
            return ServiceHandler.Handle(
                nameof(GetUsersPaginated),
                () =>
                {
                    if (contract is null)
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "Algo salio mal al procesar la solicitud.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El request no debe ser nulo.",
                            }
                        );

                    if (contract.PageSize <= 0)
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "Parámetros de paginación inválidos.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El tamaño de página debe ser mayor a 0.",
                            }
                        );

                    if (contract.CurrentPage <= 0)
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "Parámetros de paginación inválidos.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "La página actual debe ser mayor a 0.",
                            }
                        );

                    PaginatedResult<User> paginatedResult = _userUseCases.GetUsersPaginated(
                        contract.OrderIdDesc,
                        contract.PageSize,
                        contract.CurrentPage
                    );

                    List<UserDto> userDtos = _mapper.Map<List<UserDto>>(paginatedResult.Items);

                    GetUsersPaginatedResponse result = new GetUsersPaginatedResponse
                    {
                        Users = userDtos,
                        TotalUsers = paginatedResult.TotalCount,
                        CurrentPage = contract.CurrentPage,
                        PageSize = contract.PageSize
                    };

                    return Response.Success(
                        message: "Usuarios obtenidos exitosamente.",
                        code: StatusCode.Success,
                        result: result
                    );
                }
            );
        }

        public BaseResponse<GetUsersPaginatedResponse> GetUsersByNamePaginated(GetUsersByNamePaginatedContract contract)
        {
            return ServiceHandler.Handle(
                nameof(GetUsersByNamePaginated),
                () =>
                {
                    if (contract is null)
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "Algo salio mal al procesar la solicitud.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El request no debe ser nulo.",
                            }
                        );

                    if (string.IsNullOrWhiteSpace(contract.Name))
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "El nombre de búsqueda es requerido.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El campo 'name' no debe estar vacío.",
                            }
                        );

                    if (contract.PageSize <= 0)
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "Parámetros de paginación inválidos.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El tamaño de página debe ser mayor a 0.",
                            }
                        );

                    if (contract.CurrentPage <= 0)
                        return Response.Failure<GetUsersPaginatedResponse>(
                            "Parámetros de paginación inválidos.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "La página actual debe ser mayor a 0.",
                            }
                        );

                    PaginatedResult<User> paginatedResult = _userUseCases.GetUsersByNamePaginated(
                        contract.Name,
                        contract.OrderIdDesc,
                        contract.PageSize,
                        contract.CurrentPage
                    );

                    List<UserDto> userDtos = _mapper.Map<List<UserDto>>(paginatedResult.Items);

                    GetUsersPaginatedResponse result = new GetUsersPaginatedResponse
                    {
                        Users = userDtos,
                        TotalUsers = paginatedResult.TotalCount,
                        CurrentPage = contract.CurrentPage,
                        PageSize = contract.PageSize
                    };

                    return Response.Success(
                        message: "Usuarios encontrados exitosamente.",
                        code: StatusCode.Success,
                        result: result
                    );
                }
            );
        }

        public BaseResponse<UpdateUserResponse> UpdateUser(UpdateUserContract contract)
        {
            return ServiceHandler.Handle(
                nameof(UpdateUser),
                () =>
                {
                    if (contract is null)
                        return Response.Failure<UpdateUserResponse>(
                            "Algo salio mal al procesar la solicitud.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El request no debe ser nulo.",
                            }
                        );

                    if (contract.UserId <= 0)
                        return Response.Failure<UpdateUserResponse>(
                            "Identificador de usuario inválido.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El ID de usuario debe ser mayor a 0.",
                            }
                        );

                    User existingUser = _userUseCases.GetUserById(contract.UserId);

                    if (existingUser == null)
                        return Response.Failure<UpdateUserResponse>(
                            "Usuario no encontrado.",
                            StatusCode.NotFound,
                            new List<string>
                            {
                                $"No se encontró un usuario con el ID {contract.UserId}.",
                            }
                        );

                    User userToUpdate;
                    try
                    {
                        userToUpdate = _mapper.Map<User>(contract);
                    }
                    catch (AutoMapperMappingException exception)
                    {
                        throw exception.InnerException;
                    }

                    _userUseCases.UpdateUser(contract.UserId, userToUpdate);

                    UpdateUserResponse result = new UpdateUserResponse
                    {
                        UserUpdatedId = contract.UserId
                    };

                    return Response.Success(
                        message: "Usuario actualizado exitosamente.",
                        code: StatusCode.Success,
                        result: result
                    );
                }
            );
        }

        public BaseResponse<DeleteUserResponse> DeleteUser(DeleteUserContract contract)
        {
            return ServiceHandler.Handle(
                nameof(DeleteUser),
                () =>
                {
                    if (contract is null)
                        return Response.Failure<DeleteUserResponse>(
                            "Algo salio mal al procesar la solicitud.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El request no debe ser nulo.",
                            }
                        );

                    if (contract.UserId <= 0)
                        return Response.Failure<DeleteUserResponse>(
                            "Identificador de usuario inválido.",
                            StatusCode.ValidationError,
                            new List<string>
                            {
                                "El ID de usuario debe ser mayor a 0.",
                            }
                        );

                    User existingUser = _userUseCases.GetUserById(contract.UserId);

                    if (existingUser == null)
                        return Response.Failure<DeleteUserResponse>(
                            "Usuario no encontrado.",
                            StatusCode.NotFound,
                            new List<string>
                            {
                                $"No se encontró un usuario con el ID {contract.UserId}.",
                            }
                        );

                    _userUseCases.DeleteUser(contract.UserId);

                    DeleteUserResponse result = new DeleteUserResponse
                    {
                        UserDeletedId = contract.UserId
                    };

                    return Response.Success(
                        message: "Usuario eliminado exitosamente.",
                        code: StatusCode.Success,
                        result: result
                    );
                }
            );
        }
    }
}
